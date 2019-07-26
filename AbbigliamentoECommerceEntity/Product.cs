using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AbbigliamentoECommerceEntity
{
    [FirestoreData]
    public class Product
    {
        public string UId { get; set; }
        [FirestoreProperty]
        public string nome { get; set; }
        [FirestoreProperty]
        public string descrizione { get; set; }
        //quantità disponibile
        [FirestoreProperty]
        public int Quantity { get; set; }
        [FirestoreProperty]
        public string colore { get; set; }
        [FirestoreProperty]
        public string marca { get; set; }
        [FirestoreProperty]
        public string taglia { get; set; }
        [FirestoreProperty]
        public string categoria { get; set; }
        [FirestoreProperty]
        public double prezzo { get; set; }
        [FirestoreProperty]
        public string UrlDownload { get; set; }
        [FirestoreProperty]
        public string UrlDownloadWeb { get; set; }
    }
}
