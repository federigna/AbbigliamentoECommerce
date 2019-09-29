using System;
using System.Configuration;
using System.IO;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerce.Utility;
using AbbigliamentoECommerceBL;
using AbbigliamentoECommerceBL.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        //Transazione riuscita
        public async void PaymentWithPaypal()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public async void SendMail()
        {

            string wNumOrder = "12365";
            var appSettings = ConfigurationManager.AppSettings;
            string wUrlPDF = appSettings["UlrPDF"];
            FileStream wPFD = ManagementDocument.CreateOrderDocument(wUrlPDF, wNumOrder, await new CartBL().GetCartByUser("asfffkjoqejf9f"),
               null);
            //invio Mail
            bool sendEmail = false;
            try
            {
                LoggedUser wUser = new LoggedUser();
                wUser.Email = "federigna@hotmail.it";
                
                MailManagment.SendEmail(wPFD.Name, wUser);
            }
            catch (Exception ex)
            {

                Assert.Fail();

            }

            Assert.IsTrue(sendEmail);
        }
    }
}
