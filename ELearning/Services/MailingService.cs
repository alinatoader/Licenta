using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ELearning.Services
{
    public class MailingService
    {
        private static string Email = "licentavb2018@gmail.com";
        private static string Password = "Licenta123??";

        public Task SendEmailAsync(string email, string subject, string message)
        {
            MailMessage msg = new MailMessage
            {
                From = new MailAddress(Email)
            };
            msg.To.Add(email);
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com"
            };
            System.Net.NetworkCredential netc = new NetworkCredential
            {
                UserName = Email,
                Password = Password
            };
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = netc;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(msg);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
