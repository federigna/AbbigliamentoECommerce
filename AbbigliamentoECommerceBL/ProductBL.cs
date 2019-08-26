using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceBL
{
    public class ProductBL
    {
        public async Task InsertProduct(AbbigliamentoECommerceEntity.Product pProduct, string pToken)
        {
            FirebaseManegment wDB = new FirebaseManegment();
           await wDB.InsertProduct(pProduct,pToken);

        }

        public async Task<List<Product>> GetListProducts(AbbigliamentoECommerceEntity.Product pProduct)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            List<Product> wListProd= await wDB.GetProducts(pProduct,0);
            return wListProd;
        }

        public async Task<Product> GetProduct(string pId)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            Product wProd = await wDB.GetProductById(pId);
            return wProd;
        }
    }
}
