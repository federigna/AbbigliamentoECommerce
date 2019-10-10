using AbbigliamentoECommerceDB;
using AbbigliamentoECommerceEntity;
using Google.Cloud.Firestore;
using System;
using System.Threading.Tasks;

namespace AbbigliamentoECommerceBL
{
    public class UserBL
    {
        public async Task InsertUser(User pUser)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            WriteResult wwResult=await wDB.InsertUser(pUser);

        }

        public async Task<User> SigIn(string pEmail, string pPassword)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            return await wDB.SignIn(pEmail, pPassword);

        }

        public async Task<User> GetUser(string pId)
        {
            FirebaseManegment wDB = new FirebaseManegment();
            return await wDB.GetUser(pId);

        }

        public async void SetGoogleCedential()
        {
            FirebaseManegment wDB = new FirebaseManegment();
             wDB.CreateFirebaseApp();

        }
    }
}
