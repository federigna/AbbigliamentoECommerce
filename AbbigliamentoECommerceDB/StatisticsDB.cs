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
        public async Task<Dictionary<string, string>> BestSellingProduct(string pCat, DateTime pDateStart, DateTime pDateEnd, string pFieldKey)
        {

            Dictionary<string, string> wListProd = new Dictionary<string, string>();
            FirestoreDb db = new FirebaseManegment().CreateInstanceDB();
            Query allProductsQuery = db.Collection("acquisto");

            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(o => o.CreateTime.Value.ToDateTime() >= pDateStart && o.CreateTime.Value.ToDateTime() <= pDateEnd))
            {
                Dictionary<string, object> wProductArray = documentSnapshot.ContainsField("prodotti") ? documentSnapshot.GetValue<Dictionary<string, object>>("prodotti") : null;
                if (wProductArray != null)
                {
                    foreach (string wKey in wProductArray.Keys)
                    {
                        Dictionary<string, object> filed = (Dictionary<string, object>)wProductArray[wKey];
                        if (filed.ContainsKey(pFieldKey) && filed.ContainsKey("quantita"))
                        {
                            if (wListProd.ContainsKey(filed[pFieldKey].ToString().ToLower()))
                            {
                                int pQuantity = Convert.ToInt32(wListProd[filed[pFieldKey].ToString().ToLower()]);
                                pQuantity += Convert.ToInt32(filed["quantita"]);
                                wListProd[filed[pFieldKey].ToString().ToLower()] = pQuantity.ToString();
                            }
                            else
                            {
                                 wListProd.Add(filed[pFieldKey].ToString().ToLower(), filed["quantita"].ToString());
                                
                            }
                        }
                    }
                }
            }
            return wListProd;
        }

        public async Task<Dictionary<string, string>> UserRegistred(DateTime pDateStart, DateTime pDateEnd)
        {

            Dictionary<string, string> wListProd = new Dictionary<string, string>();
            FirestoreDb db = new FirebaseManegment().CreateInstanceDB();
            Query allProductsQuery = db.Collection("user");

            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(o => o.CreateTime.Value.ToDateTime() >= pDateStart && o.CreateTime.Value.ToDateTime() <= pDateEnd))
            {
                       
                if (documentSnapshot.ContainsField("Ruolo"))
                {
                    if (wListProd.ContainsKey(documentSnapshot.GetValue<string>("Ruolo").ToLower()))
                    {
                        int pQuantity = Convert.ToInt32(wListProd[documentSnapshot.GetValue<string>("Ruolo").ToLower()]);
                        pQuantity += 1;
                        wListProd[documentSnapshot.GetValue<string>("Ruolo").ToLower()] += pQuantity.ToString();
                    }
                    else
                    {
                        wListProd.Add(documentSnapshot.GetValue<string>("Ruolo").ToLower(), "1");

                    }
                }
            }
            return wListProd;
        }

        public async Task<Dictionary<string, string>> GainByBuy (string pCat, DateTime pDateStart, DateTime pDateEnd, string pFieldKey)
        {

            Dictionary<string, string> wListProd = new Dictionary<string, string>();
            FirestoreDb db = new FirebaseManegment().CreateInstanceDB();
            Query allProductsQuery = db.Collection("acquisto");

            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(o => o.CreateTime.Value.ToDateTime() >= pDateStart && o.CreateTime.Value.ToDateTime() <= pDateEnd))
            {
                Dictionary<string, object> wProductArray = documentSnapshot.ContainsField("prodotti") ? documentSnapshot.GetValue<Dictionary<string, object>>("prodotti") : null;
                if (wProductArray != null)
                {
                    foreach (string wKey in wProductArray.Keys)
                    {
                        Dictionary<string, object> filed = (Dictionary<string, object>)wProductArray[wKey];
                        if (filed.ContainsKey(pFieldKey) && filed.ContainsKey("quantita"))
                        {
                            if (wListProd.ContainsKey(filed[pFieldKey].ToString().ToLower()))
                            {
                                double pQuantity = Convert.ToInt32(wListProd[filed[pFieldKey].ToString().ToLower()]);
                                pQuantity += Convert.ToDouble(filed["prezzo"]) * Convert.ToInt32(filed["quantita"]);
                                wListProd[filed[pFieldKey].ToString().ToLower()] = pQuantity.ToString();
                            }
                            else
                            {
                                wListProd.Add(filed[pFieldKey].ToString().ToLower(), filed["prezzo"].ToString());

                            }
                        }
                    }
                }
            }
            return wListProd;
        }
    }
}
