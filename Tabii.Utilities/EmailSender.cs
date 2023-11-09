using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tabii.Utilities
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailTosend = new MimeMessage();
            emailTosend.From.Add(MailboxAddress.Parse("Hello@dotnetmastery.com"));
            emailTosend.To.Add(MailboxAddress.Parse(email));
            emailTosend.Subject = subject;
            emailTosend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            //send email
            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtb.gamil.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate("donetmastery@gmail.com", "Donet123$");
                emailClient.Send(emailTosend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
