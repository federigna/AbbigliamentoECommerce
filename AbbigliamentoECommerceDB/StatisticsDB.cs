using AbbigliamentoECommerceEntity;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace AbbigliamentoECommerceDB
{
    public class StatisticsDB
    {
        public async Task<Dictionary<string, string>> BestSellingProduct(string pCat, DateTime pDateStart, DateTime pDateEnd)
        {

            Dictionary<string, string> wListProd = new Dictionary<string, string>();
            FirestoreDb db = new FirebaseManegment().CreateInstanceDB();
            Query allProductsQuery = db.Collection("acquisto");
            //if (pCat!=null)
            //{
            //    allProductsQuery = allProductsQuery.WhereEqualTo("categoria", pCat);
            //}
            //allProductsQuery = allProductsQuery.WhereGreaterThanOrEqualTo("CreateTime", pDateStart);

            //allProductsQuery = allProductsQuery.WhereLessThanOrEqualTo("CreateTime", pDateEnd);


            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(o => o.CreateTime.Value.ToDateTime() >= pDateStart && o.CreateTime.Value.ToDateTime() <= pDateEnd))
            {
                Dictionary<string, object> wProductArray = documentSnapshot.ContainsField("prodotti") ? documentSnapshot.GetValue<Dictionary<string, object>>("prodotti") : null;
                if (wProductArray != null)
                {
                    foreach (string wKey in wProductArray.Keys)
                    {
                        Dictionary<string, object> filed = (Dictionary<string, object>)wProductArray[wKey];
                        if (filed.ContainsKey("categoria") && filed.ContainsKey("quantita"))
                        {
                            if (wListProd.ContainsKey(filed["categoria"].ToString().ToLower()))
                            {
                                int pQuantity = Convert.ToInt32(wListProd[filed["categoria"].ToString().ToLower()]);
                                pQuantity += Convert.ToInt32(filed["quantita"]);
                                wListProd[filed["categoria"].ToString().ToLower()] = pQuantity.ToString();
                            }
                            else
                            {
                                 wListProd.Add(filed["categoria"].ToString().ToLower(), filed["quantita"].ToString());
                                
                            }
                        }
                    }
                }
            }
            return wListProd;
        }
    }
}
