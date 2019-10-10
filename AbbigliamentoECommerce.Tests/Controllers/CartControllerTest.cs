using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using AbbigliamentoECommerce.Models;
using AbbigliamentoECommerce.Utility;
using AbbigliamentoECommerceBL;
using AbbigliamentoECommerceBL.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AbbigliamentoECommerce.Tests.Controllers
{
    [TestClass]
    public class CartControllerTest
    {
        

        [TestMethod]
        public async Task SendMail()
        {

            string wNumOrder = "12365";
            UserBL userBL = new UserBL();
            userBL.SetGoogleCedential();
            var appSettings = ConfigurationManager.AppSettings;
            string wUrlPDF = appSettings["UlrPDF"];
            FileStream wPFD = ManagementDocument.CreateOrderDocument(wUrlPDF, wNumOrder, await new CartBL().GetCartByUser("asfffkjoqejf9f"),
               null);
            //invio Mail
            bool sendEmail = false;
            try
            {
                string wEmail = "federigna@hotmail.it";
                string wName = "fede";
                string wsurname = "fede";

                MailManagment.SendEmail(wPFD.Name, wEmail, wName, wsurname);
            }
            catch (Exception ex)
            {

                Assert.Fail();

            }

            Assert.IsTrue(sendEmail);
        }
    }
}
