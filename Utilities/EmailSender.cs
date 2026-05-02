using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Shifaa.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("shefaaentity@gmail.com", "gvlc vkto trpa utcf")
            };

            return client.SendMailAsync(
                new MailMessage(from: "shefaaentity@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                {
                    IsBodyHtml = true
                });
        }
    
    }
}
