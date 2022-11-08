using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace BulkyBook.Utility
{
    public class Email : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse("786saqib.hussain@gmail.com"));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //Send EMail

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("786saqib.hussain@gmail.com", "035600_pk");
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
