using AbbigliamentoECommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AbbigliamentoECommerce.Utility
{
    public static class MailManagment
    {
        public static bool SendEmail( string pPathOrderPDF, string pEmailTOUser,
            string pName, string pCognome)
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string wSMTP = appSettings["SMTP"];
                string wSMTPPort = appSettings["SMTPPort"];
                string wUserMail = appSettings["UserMail"];
                string wPasswordMail = appSettings["PasswordMail"];
                string wSubjectMail = appSettings["SubjectMail"];
                string wBodyMail = appSettings["BodyMail"];

                if (pEmailTOUser != null)
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    message.From = new MailAddress(wUserMail);
                    message.To.Add(new MailAddress(pEmailTOUser));
                    message.Subject = wSubjectMail;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = wBodyMail.Replace("[NomeUtente]", pName + " " + pCognome);
                    Attachment wAttach = new Attachment(pPathOrderPDF);
                    message.Attachments.Add(wAttach);

                    smtp.Port = Convert.ToInt32(wSMTPPort);
                    smtp.Host = wSMTP; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(wUserMail, wPasswordMail);
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(message);
                    return true;
                }else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}