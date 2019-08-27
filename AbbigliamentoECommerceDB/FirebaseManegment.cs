using AbbigliamentoECommerceDB;
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
        private FirebaseApp CreateFirebaseApp()
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
        private FirestoreDb CreateInstanceDB()
        {
            var appSettings = ConfigurationManager.AppSettings;
            string project = appSettings["URLIdProject"] ?? "Not Found";

            FirestoreDb db = FirestoreDb.Create(project);
            return db;
        }
        public async Task<WriteResult> InsertUser(AbbigliamentoECommerceEntity.User pUser)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = pUser.email,
                EmailVerified = false,
                PhoneNumber = pUser.TelefoneNumber,
                Password = pUser.Password,
                DisplayName = pUser.nome,
                Disabled = false,
            };

            UserRecord userRecord = await FirebaseAdmin.Auth.FirebaseAuth.GetAuth(wApp).CreateUserAsync(args);
            // See the UserRecord reference doc for the contents of userRecord.
            //Console.WriteLine($"Successfully created new user: {userRecord.Uid}");

            //GetData
            DocumentReference wDocRef = db.Collection("users").Document(userRecord.Uid);
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();

            Dictionary<string, object> wDictionaryUser = new Dictionary<string, object>
                {
                    { "Address",pUser.Address },
                    { "City",pUser.City },
                    { "DateOfBirth",pUser.DateOfBirth },
                    { "District",pUser.District },
                    { "email",pUser.email },
                    { "cognome",pUser.cognome },
                    { "nome",pUser.nome },
                    { "TelefoneNumber",pUser.TelefoneNumber },
                    { "Ruolo","Cliente" },
                };
            WriteResult wWResult = await wDocRef.SetAsync(wDictionaryUser);

            //End get Data

            return wWResult;
        }
        public async Task InsertProduct(Product pProduct, string pToken)
        {
            var appSettings = ConfigurationManager.AppSettings;

            string wAppSpot = appSettings["StorageAppspot"] ?? "Not Found";

            FirestoreDb db = CreateInstanceDB();
            // Get any Stream - it can be FileStream, MemoryStream or any other type of Stream
            var stream = File.Open(pProduct.UrlDownload, FileMode.Open);


            var app =
              new FirebaseStorageOptions()
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(pToken)
              };
            // Constructr FirebaseStorage, path to where you want to upload the file and Put it there
            var task = new FirebaseStorage(wAppSpot, app)
                .Child(pProduct.categoria)
                .Child(pProduct.marca)
                .Child(stream.Name)
                .PutAsync(stream);

            // await the task to wait until upload completes and get the download url
            var downloadUrl = await task;

            DocumentReference wDocRef = db.Collection("prodotto").Document();
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();

            Dictionary<string, object> wDictionaryProd = new Dictionary<string, object>
                {
                    { "categoria",pProduct.categoria },
                    { "marca",pProduct.marca },
                    { "prezzo",pProduct.prezzo },
                    { "nome",pProduct.nome },
                    { "taglia",pProduct.taglia },
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
                
            }
           

            return wProd;
        }
        public async Task<List<Product>> GetProducts(Product product, int pLimit)
        {
            pLimit = pLimit == 0 ? 4 : pLimit + 4;

            List<Product> wListProd = new List<Product>();
            FirestoreDb db = CreateInstanceDB();
            Query allProductsQuery = null;
            if (pLimit == 4)
            {
                allProductsQuery = db.Collection("prodotto").Limit(pLimit);

            }
            else
            {
                allProductsQuery = db.Collection("prodotto").StartAfter(pLimit);

            }
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
            if (product.nome == null)
            {
                product.nome = string.Empty;
            }
            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents.Where(x => x.GetValue<string>("nome").Contains(product.nome)))
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
        public async Task<WriteResult> AddProductToCart(Product pProduct, string pUserUId, int pQuantitySelect)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();

            //GetData
            DocumentReference wDocRef = db.Collection("user").Document(pUserUId);
            DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
            Dictionary<string, object> wMapCart = new Dictionary<string, object>();
            wMapCart.Add("quanita", pQuantitySelect);
            wMapCart.Add("uidProdotto", pProduct.UId);

            WriteResult wWResult = await wDocRef.UpdateAsync("carrello", FieldValue.ArrayUnion(wMapCart));

            //End get Data

            return wWResult;
        }
        public async Task<WriteResult> AddHistoryBuy(List<Product> pProduct, string pUserUId)
        {

            FirestoreDb db = CreateInstanceDB();
            FirebaseApp wApp = CreateFirebaseApp();

            //Object Map è una Dictionary

            DocumentReference wDocRef = db.Collection("acquisto").Document();
            Dictionary<string, object> wAcquisto = new Dictionary<string, object>();
            Dictionary<string, object> wSingleProd = new Dictionary<string, object>();
            int i = 1;
            foreach (Product wProd in pProduct)
            {

                wSingleProd.Add(i.ToString(), wProd);
                i++;
            }
            wAcquisto.Add(pUserUId, wSingleProd);

            WriteResult wWResult = await wDocRef.SetAsync(wAcquisto);

            //End get Data

            return wWResult;
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

                //Effettuo la login tramite email e password
                //FirebaseAuthLink auth = await authProvider.SignInWithEmailAndPasswordAsync(pEmail, pPassword);

                //recupero del uid utente per recuperare tutte le info del'utente loggato
                //var decoded = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(auth.FirebaseToken);
                //var uid = decoded.Uid;
                DocumentReference wDocRef = db.Collection("users").Document(userId);

                DocumentSnapshot snapshot = await wDocRef.GetSnapshotAsync();
                if (snapshot.Exists)
                {
                    AbbigliamentoECommerceEntity.Carrello[] wCartArray = snapshot.ContainsField("carrello") ? snapshot.GetValue<Carrello[]>("carrello") : null;
                    if (wCartArray != null)
                    {
                        foreach (Carrello wCartDB in wCartArray)
                        {

                            DocumentReference wDocRefProd = db.Collection("prodotto").Document(wCartDB.uidProdotto);
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
                                wCartDetail.quantita = wCartDB.quantita;
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

        public async Task<ResultStatics> StatisticsResult(FilterStatics pFilter, string pTableName)
        {
            FirebaseApp wApp = CreateFirebaseApp();
            FirestoreDb db = CreateInstanceDB();
            Query allProductsQuery = db.Collection(pTableName);
            QuerySnapshot allProductQuerySnapshot = await allProductsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allProductQuerySnapshot.Documents)
            {
                if (documentSnapshot.Exists)
                {

                    Category wProd = new Category();
                    wProd.Id = documentSnapshot.Id;
                    wProd.Description = documentSnapshot.Id;


                }
                else
                {

                }
            }
            return null;
        }
    }

}
