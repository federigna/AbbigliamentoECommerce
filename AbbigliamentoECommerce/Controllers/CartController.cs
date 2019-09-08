using AbbigliamentoECommerce.Converter;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerce.Utility;
using AbbigliamentoECommerceBL;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AbbigliamentoECommerce.Controllers
{
    public class CartController : Controller
    {
        public LoggedUser wLogUser = null;
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Cart/Details/5
        public async Task<ActionResult> Details()
        {
            wLogUser = (LoggedUser)Session["CurrentUser"];
            Cart wCart = ConvertEntityUserTOUserModel.ConvertoCartEntityTOCartModel(await new CartBL().GetCartByUser(wLogUser.wDetailUser.Id));
            wCart.UserOwner = wLogUser.wDetailUser;
            return View("Details", wCart);
        }

        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cart/Delete/5
        public async Task<ActionResult> DeleteProduct(string idProduct)
        {
            LoggedUser wUser = (LoggedUser)Session["CurrentUser"];
            Google.Cloud.Firestore.WriteResult wResult = await new CartBL().RemoveProductToCart(wUser.wDetailUser.Id, idProduct);

            return await Details();
        }

        // POST: Cart/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> PaymentWithPaypal(string userId, string Cancel = null)
        {
            //getting the apiContext
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            wLogUser = (LoggedUser)Session["CurrentUser"];
            try
            {

                //A resource representing a Payer that funds a payment Payment Method as paypal
                //Payer Id will be returned when payment proceeds or click to pay
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority +
                                "/Cart/PaymentWithPayPal?";

                    //here we are generating guid for storing the paymentID received in session
                    //which will be used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment
                    Cart wCart = ConvertEntityUserTOUserModel.ConvertoCartEntityTOCartModel(await new CartBL().GetCartByUser(wLogUser.wDetailUser.Id));
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, wCart);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {

                    // This function exectues after receving all parameters for the payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    //If executed payment failed then we will show payment failure message to user
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                return View("FailureView");
            }

            //on successful payment, show success page to user.
            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, Cart pCart)
        {
            //create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            foreach (DetailCart wDetail in pCart.DetailsCart)
            {
                //Adding Item Details like name, currency, price etc
                itemList.items.Add(new Item()
                {
                    name = wDetail.Product.ProductName,
                    currency = "USD",
                    price = wDetail.Product.Price.ToString("0.##"),
                    quantity = wDetail.Quantity == 0 ? "1" : wDetail.Quantity.ToString(),
                    sku = "sku"
                });

            }
            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };

            Details details=null;
            //il details deve essere presente solo se si hanno più elementi
            if (itemList.items.Count > 1)
            {
                // Adding Tax, shipping and Subtotal details
                 details = new Details()
                {
                    tax = pCart.Vat.ToString("0.##"),
                    shipping = pCart.ShippingCost.ToString("0.##"),
                    subtotal = pCart.TotalPrice.ToString("0.##")
                };
            }
            //Final amount with details
            var amount = new Amount()
            {
                currency = "USD",
                total =details!=null? details.tax + details.shipping + details.subtotal: pCart.TotalPrice.ToString("0.##"), // Total must be equal to sum of tax, shipping and subtotal.
                details = details
            };


            var transactionList = new List<Transaction>();
            // Adding description about the transaction
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "1",
                amount = amount,
                item_list = itemList
            });


            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        public ActionResult SuccessView()
        {
            try
            {
                //wLogUser = (LoggedUser)Session["CurrentUser"];
                //scrittura pdf di conferma acquisto

                //creo lo storico dell'ordine
                //new CartBL().AddHistoryBuy(wLogUser.Id);
              

                //Invio mail
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
