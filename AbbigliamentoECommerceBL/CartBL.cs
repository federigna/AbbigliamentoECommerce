using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
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
    }
}
