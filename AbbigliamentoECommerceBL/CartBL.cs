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

        public async Task<WriteResult> AddHistoryBuy(string pUserId)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            Cart wCart = await wDB.GetCartByUser(pUserId);
            List<Product> wListProd = new List<Product>();
            foreach(CartDetail wProd in wCart.listProduct)
            {
                wListProd.Add(wProd.singleProduct);
            }
            return await wDB.AddHistoryBuy(wListProd, pUserId);

        }

        public FileStream CreatePDF()
        {
            return  ManagementDocument.CreateOrderDocument("C:\\Users\\feder\\Downloads", "00001",null,null);

        }

    }
}
