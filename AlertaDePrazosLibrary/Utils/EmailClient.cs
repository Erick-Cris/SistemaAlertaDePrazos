﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Utils
{
    public class EmailClient
    {
        public static void EnviarEmail(string assunto, string corpo, string destinatario)
        {
            MailMessage mail = new MailMessage("erickcristian@outlook.com", destinatario);
            SmtpClient client = new SmtpClient();

            client.EnableSsl = true;
            client.Host = "smtp.office365.com";
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("erickcristian@outlook.com", "password");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            mail.Subject = assunto;
            mail.Body = corpo;
            mail.IsBodyHtml = true;

            client.Send(mail);
        }
    }
}
