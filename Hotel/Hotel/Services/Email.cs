using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
namespace Hotel.Services
{
    public class Email
    {
        public void Sending(string to_email, string username)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("saichandra.ts@gmail.com", "nwzz mkeb eltv gufh");

            // Create email message
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("saichandra.ts@gmail.com");
            mailMessage.To.Add(to_email);
            mailMessage.Subject = "Regaring Booking Hotel";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat("<h1>Room Booked</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"Dear {username}");
            mailBody.AppendFormat("<p>Thank you For Booking Hotel</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
