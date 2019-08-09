using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
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
            AbbigliamentoECommerceEntity.Cart wCart= await wDB.GetCartByUser(pUserId);
            return wCart;

        }
    }
}
