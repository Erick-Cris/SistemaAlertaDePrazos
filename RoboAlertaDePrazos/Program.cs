// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Mail;

try
{
    MailMessage mail = new MailMessage("erickcristianup@outlook.com", "erickcristianup@gmail.com");
    SmtpClient client = new SmtpClient();

    client.EnableSsl = true;
    client.Host = "smtp.office365.com";
    client.UseDefaultCredentials = false;
    client.Credentials = new System.Net.NetworkCredential("erickcristianup@outlook.com", "o]3GY6r/xG]K");

    client.Port = 587;
    client.DeliveryMethod = SmtpDeliveryMethod.Network;

    mail.Subject = "teste";
    mail.Body = "teste2";

    client.Send(mail);

    Console.WriteLine("Sucesso");
    while (true) { }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
    while (true) { }
}
