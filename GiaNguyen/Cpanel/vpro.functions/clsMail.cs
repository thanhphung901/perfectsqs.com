using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;

namespace vpro.functions
{
    public class clsMail
    {

        public static string SendMailNet(string toAddress, string ccAddress, string bccAddress, string subject, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(FormAddress, System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]);
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
            return "Send email successful!";
        }

        public static string FormAddress
        {
            get
            {
                SmtpSection cfg = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
                return cfg.Network.UserName;
            }
        }

    }
}
