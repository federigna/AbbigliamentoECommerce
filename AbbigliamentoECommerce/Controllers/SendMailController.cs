using AbbigliamentoECommerce.Utility;
using AbbigliamentoECommerceBL;
using AbbigliamentoECommerceBL.Utility;
using AbbigliamentoECommerceEntity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbbigliamentoECommerce.Controllers
{

    public class SendMailController : ApiController
    {
        [HttpGet]
        public async void SendMail(string pNumOrder, string pIdUser)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string wUrlPDF = appSettings["UlrPDF"];
            Log.Info("REST:Scrittura PDF per ordine " + pNumOrder);
            User wUser = await new UserBL().GetUser(pIdUser);
            FileStream wPFD = ManagementDocument.CreateOrderDocument(wUrlPDF, pNumOrder, await new CartBL().GetCartByUser(pIdUser),
               wUser);
            //invio Mail
            try
            {
                Log.Info("REST:Tentativo invio mail per ordine " + pNumOrder);
                MailManagment.SendEmail(wPFD.Name, wUser.email,wUser.nome,wUser.cognome);
            }
            catch (Exception ex)
            {

                Log.Error("REST:Errore durante l'invio della mail.", ex);

            }
        }
    }
}
