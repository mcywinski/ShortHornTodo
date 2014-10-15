using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web;
using System.Configuration;

namespace ShortHorn.Services
{
    /// <summary>
    /// Handles and automates E-Mail communication
    /// Also contains message definitions
    /// </summary>
    public class ExternalMailService : IMailService
    {
        /// <summary>
        /// Sends E-Mail message to a single recipent
        /// </summary>
        /// <param name="to">Recipent's address</param>
        /// <param name="title">Message title</param>
        /// <param name="contents">Message body</param>
        /// <param name="from">Author's address. If null or empty string is passed, a default value will be used instead.</param>
        /// <returns>Positive value is returned in case of success, negative otherwise./returns>
        public bool SendMail(string to, string title, string contents, string from = null)
        {
            SmtpClient client = new SmtpClient();
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultMailSMTPPort"]);
            client.Host = ConfigurationManager.AppSettings["DefaultMailSMTPServer"];
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["DefaultMailUser"], ConfigurationManager.AppSettings["DefaultMailPassword"]);

            MailMessage mm = new MailMessage(string.IsNullOrEmpty(from) ? ConfigurationManager.AppSettings["DefaultNoReplyEmailAddress"] : from, to, title, contents);
            mm.BodyEncoding = System.Text.UTF8Encoding.UTF8;
            //Optional mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                LogService.Logger.Error("Error while sending registration message.", ex);
                return false;
            }

            return true;
        }
    }
}