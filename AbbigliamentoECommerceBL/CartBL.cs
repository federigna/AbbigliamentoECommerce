using AbbigliamentoECommerceBL.Utility;
using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceBL
{
    public class CartBL
    {
        public async Task<Cart> GetCartByUser(string pUserId)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            AbbigliamentoECommerceEntity.Cart wCart = await wDB.GetCartByUser(pUserId);
            return wCart;

        }

        public async Task<WriteResult> AddProductToCart(string pUserId, string pIdProd, int pQauntityBuy)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            AbbigliamentoECommerceEntity.Product wProd = await wDB.GetProductById(pIdProd);
            return await wDB.AddProductToCart(wProd, pUserId, pQauntityBuy);
            
        }

        public async Task<WriteResult> RemoveProductToCart(string pUserId, string pIdProd)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            return await wDB.RemoveProductToCart(pIdProd, pUserId);

        }

        public async Task<WriteResult> AddHistoryBuy(string pUserId, string pNumOrder)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            Cart wCart = await wDB.GetCartByUser(pUserId);
            List<Product> wListProd = new List<Product>();
            foreach(CartDetail wProd in wCart.listProduct)
            {
                Product wPrdoCart = new Product();
                wPrdoCart = wProd.singleProduct;
                wPrdoCart.Quantity = wProd.quantita;
                wListProd.Add(wPrdoCart);
            }
            return await wDB.AddHistoryBuy(wListProd, pUserId,Convert.ToInt32(pNumOrder));

        }

        public FileStream CreatePDF()
        {
            return  ManagementDocument.CreateOrderDocument("C:\\Users\\feder\\Downloads", "00001",null,null);

        }

        public int GenerateNumOrder()
        {
            string wDate = DateTime.Now.Year.ToString().Substring(2, 2) + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            return Convert.ToInt32(wDate )+ DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
        }

    }
}
