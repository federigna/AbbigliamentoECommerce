﻿using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using Firebase.Auth;
using Firebase.Storage;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceDB
{
    public class FirebaseManegment
    {
        //Crea il FirebaseApp tramite il nome del DB configurato da webConfig
        //Per creare l'istanza di questo DB si utilizzano le Default Credential se
        //queste sono già utilizzate, altrimenti si recuperano del file json di configurazione
        //che si trova al percorso indicato dal URLFirestoreDB.
        public FirebaseApp CreateFirebaseApp()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string result = appSettings["URLFirestoreDB"] ?? "Not Found";

            FirebaseApp wApp = FirebaseApp.DefaultInstance;
            if (wApp == null)
            {
                wApp = FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(result),
                });
            }
            if (string.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS")))
            {
                System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", result);
            }
            return wApp;
        }

        public FirestoreDb CreateInstanceDB()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string project = appSettings["URLIdProject"] ?? "Not Found";

            FirestoreDb db = FirestoreDb.Create(project);
            return db;
        }

        public async Task<WriteResult> InsertUser(AbbigliamentoECommerceEntity.User pUser)
        {
            FirebaseApp wApp = CreateFirebaseApp();
            FirestoreDb db = CreateInstanceDB();
            var appSettings = ConfigurationManager.AppSettings;
            string apiKey = appSettings["FirebaseApiKey"] ?? "Not Found";
            //per creare l'username e la password di accesso all'applicazione
            //è necessario utilizzare UserRecordArgs valorizzando le properties con i valori inseriti
            //dall'utente di fase di registrazione
            //si è deciso di usare un autenticazione di tipo email e password
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = pUser.email,
                EmailVerified = false,
                PhoneNumber = pUser.TelefoneNumber,
                Password = pUser.Password,
                DisplayName = pUser.nome,
                Disabled = false,
            };
            //viene creato un record nell'Authentication di Firebase tramite un account amministrativ
            UserRecord userRecord = await FirebaseAdmin.Auth.FirebaseAuth.GetAuth(wApp).CreateUserAsync(args);
            
           // se la registrazione dell'email e password avviene correttamente si procederà
           //accedendo al catalogo User e compilando le informazioni mancanti
            //GetData
            DocumentReference wDocRef = db.Collection("user").Document(userRecord.Uid);
            
            Dictionary<string, object> wDictionaryUser = new Dictionary<string, object>
                {
                    { "Address",pUser.Address },
                    { "City",pUser.City },
                    //{ "DateOfBirth",pUser.DateOfBirth },
                    { "District",pUser.District },
                    { "email",pUser.email },
                    { "cognome",pUser.cognome },
                    { "nome",pUser.nome },
                    { "TelefoneNumber",pUser.TelefoneNumber },
                    { "Ruolo","Cliente" },
                };
            return await wDocRef.SetAsync(wDictionaryUser);
            
        }
        public async Task InsertProduct(Product pProduct, string pToken)
        {
            var appSettings = ConfigurationManager.AppSettings;
            //per il caricamento del prodotto è necessario prima caricare l'immagine nello Storage
            //il cui percorso è stato configurato da web.config StorageAppspot
            string wAppSpot = appSettings["StorageAppspot"] ?? "Not Found";

            FirestoreDb db = CreateInstanceDB();
            // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(pProduct.UrlDownloadWeb, FileMode.Open);


            // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
            var task = new FirebaseStorage(wAppSpot)
                .Child(pProduct.modello.ToLower())
                .Child(pProduct.marca.ToLower())
                .Child(stream.Name)
                .PutAsync(stream);
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");
            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;

            //se l'inserimento dell'immagine va a buon fine verrà recuperata la WebDownloadUrl 
            //che verrà salvata nel campo di riferimento nel catalogo prodotto
            DocumentReference wDocRef = db.Collection("prodotto").Document();
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();

            Dictionary<string, object> wDictionaryProd = new Dictionary<string, object>
                {
                    { "categoria",pProduct.categoria },
                    { "marca",pProduct.marca },
                    { "prezzo",pProduct.prezzo },
                    { "nome",pProduct.nome },
                    { "colore",pProduct.colore },
                    { "taglia",pProduct.taglia },
                    { "modello",pProduct.modello },
                    {"Quantity",pProduct.Quantity },
                    { "urlDownload",stream.Name },
                    { "urlDownloadWeb",downloadUrl },
                };
            WriteResult wWResult = await wDocRef.SetAsync(wDictionaryProd);
        }
        public async Task<List<Product>> GetProducts()
        {
            List<Product> wListProd = new List<Product>();
            FirestoreDb db = CreateInstanceDB();
            Query allProductsQuery = db.Collection("prodotto");
            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {

                    Product wProd = documentSnapshot.ConvertTo<Product>();
                    wListProd.Add(wProd);

                }
                else
                {

                }
            }
            return wListProd;
        }
        //recupera il prodotto tramite uid per la gestione del dettaglio prodotto
        public async Task<Product> GetProductById(string pId)
        {
            Product wProd = new Product();
            FirestoreDb db = CreateInstanceDB();
            DocumentReference wDocRef = db.Collection("prodotto").Document(pId);
            DocumentSnapshot documentSnapshot = await wDocRef.GetSnapshotAsync();
            if (documentSnapshot.Exists)
            {
                wProd.UId = documentSnapshot.Id;
                wProd.categoria = documentSnapshot.ContainsField("categoria") ? documentSnapshot.GetValue<string>("categoria") : "";
                wProd.colore = documentSnapshot.ContainsField("colore") ? documentSnapshot.GetValue<string>("colore") : "";
                wProd.marca = documentSnapshot.ContainsField("marca") ? documentSnapshot.GetValue<string>("marca") : "";
                wProd.nome = documentSnapshot.ContainsField("nome") ? documentSnapshot.GetValue<string>("nome") : "";
                wProd.prezzo = documentSnapshot.ContainsField("prezzo") ? documentSnapshot.GetValue<double>("prezzo") : 0;
                wProd.taglia = documentSnapshot.ContainsField("taglia") ? documentSnapshot.GetValue<string>("taglia") : "";
                wProd.UrlDownloadWeb = documentSnapshot.ContainsField("urlDownloadWeb") ? documentSnapshot.GetValue<string>("urlDownloadWeb") : "";
                wProd.descrizione = documentSnapshot.ContainsField("descrizione") ? documentSnapshot.GetValue<string>("descrizione") : "";
                wProd.Quantity = documentSnapshot.ContainsField("Quantity") ? documentSnapshot.GetValue<int>("Quantity") : 0;
            }


            return wProd;
        }
        //recupera i prodotti in base ai filtri passati come parametro  tramite Product
        //limit indica il numero massimo di elementi da visualizzare per pagina
        public async Task<List<Product>> GetProducts(Product product, int pLimit)
        {
            ///pLimit = pLimit == 0 ? 4 : pLimit + 4;

            List<Product> wListProd = new List<Product>();
            FirestoreDb db = CreateInstanceDB();
            Query allProductsQuery = db.Collection("prodotto");
            //if (pLimit == 4)
            //{
            //    allProductsQuery = db.Collection("prodotto").Limit(pLimit);

            //}
            //else
            //{
            //    allProductsQuery = db.Collection("prodotto").StartAfter(pLimit);

            //}
            if (!string.IsNullOrEmpty(product.categoria))
            {
                allProductsQuery = allProductsQuery.WhereEqualTo("categoria", product.categoria);
            }
            if (!string.IsNullOrEmpty(product.marca))
            {
                allProductsQuery = allProductsQuery.WhereEqualTo("marca", product.marca);
            }
            if (!string.IsNullOrEmpty(product.taglia))
            {
                allProductsQuery = allProductsQuery.WhereEqualTo("taglia", product.taglia);
            }
            if (!string.IsNullOrEmpty(product.colore))
            {
                allProductsQuery = allProductsQuery.WhereEqualTo("colore", product.colore);
            }
            if (!string.IsNullOrEmpty(product.modello))
            {
                allProductsQuery = allProductsQuery.WhereEqualTo("modello", product.modello);
            }
            if (product.nome == null)
            {
                product.nome = string.Empty;
            }
            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(x => x.GetValue<string>("nome").ToLower().Contains(product.nome.ToLower())))
            {
                Product wProd = new Product();
                wProd.UId = documentSnapshot.Id;
                wProd.categoria = documentSnapshot.ContainsField("categoria") ? documentSnapshot.GetValue<string>("categoria") : "";
                wProd.colore = documentSnapshot.ContainsField("colore") ? documentSnapshot.GetValue<string>("colore") : "";
                wProd.marca = documentSnapshot.ContainsField("marca") ? documentSnapshot.GetValue<string>("marca") : "";
                wProd.nome = documentSnapshot.ContainsField("nome") ? documentSnapshot.GetValue<string>("nome") : "";
                wProd.prezzo = documentSnapshot.ContainsField("prezzo") ? documentSnapshot.GetValue<double>("prezzo") : 0;
                wProd.taglia = documentSnapshot.ContainsField("taglia") ? documentSnapshot.GetValue<string>("taglia") : "";
                wProd.UrlDownloadWeb = documentSnapshot.ContainsField("urlDownloadWeb") ? documentSnapshot.GetValue<string>("urlDownloadWeb") : "";
                wProd.descrizione = documentSnapshot.ContainsField("descrizione") ? documentSnapshot.GetValue<string>("descrizione") : "";
                wListProd.Add(wProd);


            }
            return wListProd;
        }
        //aggiunge un elemento al carrello
        public async Task<WriteResult> AddProductToCart(Product pProduct, string pUserUId, int pQuantitySelect)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();

         //per prima cosa viene recuperato il record User a cui è associato 
         //un elemento di tipo Map relativo al carrello
            DocumentReference wDocRef = db.Collection("user").Document(pUserUId);
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
            
            Dictionary<string, object> wMapCart = new Dictionary<string, object>();
            wMapCart.Add("quantita", pQuantitySelect);
            wMapCart.Add("uidProdotto", pProduct.UId);

            WriteResult wWResult = await wDocRef.UpdateAsync("carrello", FieldValue.ArrayUnion(wMapCart));

            
            return wWResult;
        }
        public async Task<WriteResult> RemoveProductToCart(string pProductId, string pUserUId)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();

            //GetData
            DocumentReference wDocRef = db.Collection("user").Document(pUserUId);
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
            WriteResult wWResult = null;
            Dictionary<string, object>[] wCartArray = snapshot.ContainsField("carrello") ? snapshot.GetValue<Dictionary<string, object>[]>("carrello") : null;
            if (wCartArray != null)
            {
                foreach (Dictionary<string, object> wCartDBColl in wCartArray)
                {

                    if (wCartDBColl["uidProdotto"].ToString() == pProductId)
                    {
                         wWResult = await wDocRef.UpdateAsync("carrello", FieldValue.ArrayRemove(wCartDBColl));
                    }

                }
            }
            //End get Data

            return wWResult;
        }
        //ad acquisto effettuato con successo verrà creato un record nel catalogo
        //Acquisto con la copia del carrello e dei relativi prodotti acquistati.
        public async Task<WriteResult> AddHistoryBuy(List<Product> pProduct, string pUserUId, int pNumOrdine)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();

            //Object Map è una Dictionary

            DocumentReference wDocRef = db.Collection("acquisto").Document();
            Dictionary<string, object> wAcquisto = new Dictionary<string, object>();
            Dictionary<string, object> wSingleProd = new Dictionary<string, object>();
            int i = 0;
            foreach (Product wProd in pProduct)
            {
                Dictionary<string, object> wDictionaryProd = new Dictionary<string, object>
                {
                    { "categoria",wProd.categoria },
                    { "marca",wProd.marca },
                    { "prezzo",wProd.prezzo },
                     { "modello",wProd.modello },
                    { "nome",wProd.nome },
                    { "taglia",wProd.taglia },
                    {"quantita",wProd.Quantity },
                };
                wSingleProd.Add(i.ToString(), wDictionaryProd);
                i++;
            }
            wAcquisto.Add("uidUtente", pUserUId);
            wAcquisto.Add("numOrdine", pNumOrdine);
            wAcquisto.Add("prodotti", wSingleProd);

            WriteResult wResult= await wDocRef.SetAsync(wAcquisto);

            try
            {
                //se la scrittura del record Acquisto viene effettuata correttamente
                //Svuoto il carrello
                foreach (Product wProd in pProduct)
                {
                    await RemoveProductToCart(wProd.UId, pUserUId);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
           

            return wResult;
        }

        public async Task<List<Category>> GetCategory(string pNameRaccolta)
        {
            List<Category> wListProd = new List<Category>();
            FirebaseApp wApp = CreateFirebaseApp();
            FirestoreDb db = CreateInstanceDB();
            Query allProductsQuery = db.Collection(pNameRaccolta);
            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {

                    Category wProd = new Category();
                    wProd.Id = documentSnapshot.Id;
                    wProd.Description = documentSnapshot.Id;
                    wListProd.Add(wProd);

                }
                else
                {

                }
            }
            return wListProd;
        }

        public async Task<Cart> GetCartByUser(string userId)
        {
            FirestoreDb db = CreateInstanceDB();

            var appSettings = ConfigurationManager.AppSettings;
            string wApiKey = appSettings["FirebaseApiKey"] ?? "Not Found";
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(wApiKey));
            AbbigliamentoECommerceEntity.Cart wCart = new AbbigliamentoECommerceEntity.Cart();

            wCart.listProduct = new List<CartDetail>();
            try
            {
                
                //uso il uid utente per recuperare tutte le info dell'utente loggato
                DocumentReference wDocRef = db.Collection("user").Document(userId);

                DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    Dictionary<string, object>[] wCartArray = snapshot.ContainsField("carrello") ? snapshot.GetValue<Dictionary<string, object>[]>("carrello") : null;
                    if (wCartArray != null)
                    {
                        foreach (Dictionary<string, object> wCartDBColl in wCartArray)
                        {
                            DocumentReference wDocRefProd = db.Collection("prodotto").Document(wCartDBColl["uidProdotto"].ToString());
                            DocumentSnapshot snapshotProd = await wDocRefProd.GetSnapshotAsync();
                            if (snapshotProd.Exists)
                            {
                                AbbigliamentoECommerceEntity.CartDetail wCartDetail = new AbbigliamentoECommerceEntity.CartDetail();
                                Product wProd = new Product();
                                wProd.UId = snapshotProd.Id;
                                wProd.categoria = snapshotProd.ContainsField("categoria") ? snapshotProd.GetValue<string>("categoria") : "";
                                wProd.colore = snapshotProd.ContainsField("colore") ? snapshotProd.GetValue<string>("colore") : "";
                                wProd.marca = snapshotProd.ContainsField("marca") ? snapshotProd.GetValue<string>("marca") : "";
                                wProd.nome = snapshotProd.ContainsField("nome") ? snapshotProd.GetValue<string>("nome") : "";
                                wProd.prezzo = snapshotProd.ContainsField("prezzo") ? snapshotProd.GetValue<double>("prezzo") : 0;
                                wProd.taglia = snapshotProd.ContainsField("taglia") ? snapshotProd.GetValue<string>("taglia") : "";
                                wProd.UrlDownloadWeb = snapshotProd.ContainsField("urlDownloadWeb") ? snapshotProd.GetValue<string>("urlDownloadWeb") : "";
                                wProd.descrizione = snapshotProd.ContainsField("descrizione") ? snapshotProd.GetValue<string>("descrizione") : "";
                                wProd.Quantity = snapshotProd.ContainsField("Quantity") ? snapshotProd.GetValue<int>("Quantity") : 0;
                                wCartDetail.quantita = Convert.ToInt32(wCartDBColl["quantita"]);
                                wCartDetail.singleProduct = wProd;
                                wCart.listProduct.Add(wCartDetail);
                            }

                        }
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return wCart;
        }

        //Metodo che effettua la login e verifica utenza di Firebase
        public async Task<AbbigliamentoECommerceEntity.User> SignIn(string pEmail, string pPassword)
        {
            FirestoreDb db = CreateInstanceDB();
            AbbigliamentoECommerceEntity.User wUser = new AbbigliamentoECommerceEntity.User();
            var appSettings = ConfigurationManager.AppSettings;
            string wApiKey = appSettings["FirebaseApiKey"] ?? "Not Found";
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(wApiKey));
            try
            {

                //Effettuo la login tramite email e password
                FirebaseAuthLink auth = await authProvider.SignInWithEmailAndPasswordAsync(pEmail, pPassword);

                //recupero del uid utente per recuperare tutte le info del'utente loggato
                var decoded = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(auth.FirebaseToken);
                var uid = decoded.Uid;
                DocumentReference wDocRef = db.Collection("user").Document(uid);

                DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    wUser.Address = snapshot.ContainsField("Address") ? snapshot.GetValue<string>("Address") : "";
                    wUser.City = snapshot.ContainsField("City") ? snapshot.GetValue<string>("City") : "";
                    wUser.cognome = snapshot.ContainsField("cognome") ? snapshot.GetValue<string>("cognome") : "";
                    wUser.DateOfBirth = snapshot.ContainsField("DateOfBirth") ? snapshot.GetValue<DateTime>("DateOfBirth") : DateTime.MinValue;
                    wUser.District = snapshot.ContainsField("District") ? snapshot.GetValue<string>("District") : "";
                    wUser.email = snapshot.ContainsField("email") ? snapshot.GetValue<string>("email") : "";
                    wUser.Id = uid;
                    wUser.nome = snapshot.ContainsField("nome") ? snapshot.GetValue<string>("nome") : "";
                    wUser.Ruolo = snapshot.ContainsField("Ruolo") ? snapshot.GetValue<string>("Ruolo") : "cliente";
                    wUser.TelefoneNumber = snapshot.ContainsField("TelefoneNumber") ? snapshot.GetValue<string>("TelefoneNumber") : "";

                }

            }
            catch (Exception)
            {

                throw;
            }
            return wUser;
        }

        public async Task<AbbigliamentoECommerceEntity.User> GetUser(string pId)
        {
            FirestoreDb db = CreateInstanceDB();
            AbbigliamentoECommerceEntity.User wUser = new AbbigliamentoECommerceEntity.User();
            var appSettings = ConfigurationManager.AppSettings;
            string wApiKey = appSettings["FirebaseApiKey"] ?? "Not Found";
           // var authProvider = new FirebaseAuthProvider(new FirebaseConfig(wApiKey));
            try
            {
                //recupero del uid utente per recuperare tutte le info del'utente loggato
                 DocumentReference wDocRef = db.Collection("user").Document(pId);

                DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    wUser.Address = snapshot.ContainsField("Address") ? snapshot.GetValue<string>("Address") : "";
                    wUser.City = snapshot.ContainsField("City") ? snapshot.GetValue<string>("City") : "";
                    wUser.cognome = snapshot.ContainsField("cognome") ? snapshot.GetValue<string>("cognome") : "";
                    wUser.DateOfBirth = snapshot.ContainsField("DateOfBirth") ? snapshot.GetValue<DateTime>("DateOfBirth") : DateTime.MinValue;
                    wUser.District = snapshot.ContainsField("District") ? snapshot.GetValue<string>("District") : "";
                    wUser.email = snapshot.ContainsField("email") ? snapshot.GetValue<string>("email") : "";
                    wUser.Id = pId;
                    wUser.nome = snapshot.ContainsField("nome") ? snapshot.GetValue<string>("nome") : "";
                    wUser.Ruolo = snapshot.ContainsField("Ruolo") ? snapshot.GetValue<string>("Ruolo") : "cliente";
                    wUser.TelefoneNumber = snapshot.ContainsField("TelefoneNumber") ? snapshot.GetValue<string>("TelefoneNumber") : "";

                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return wUser;
        }

        public void SignOut()
        {
            FirestoreDb db = CreateInstanceDB();
            AbbigliamentoECommerceEntity.User wUser = new AbbigliamentoECommerceEntity.User();
            var appSettings = ConfigurationManager.AppSettings;
            string wApiKey = appSettings["FirebaseApiKey"] ?? "Not Found";
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(wApiKey));
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }

}
