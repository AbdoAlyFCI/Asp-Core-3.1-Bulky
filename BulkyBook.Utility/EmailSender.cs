using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOption emailOptions;
        public EmailSender(IOptions<EmailOption> options)
        {
            emailOptions = options.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(emailOptions.SenderGridKey,subject,htmlMessage,email);
        }

        private Task Execute(string sendGridKey,string subject,string message,string email)
        {
            var client = new SendGridClient(sendGridKey);
            var from = new EmailAddress("speed4392014@gmail.com", "Bulky Abdo");
            var to = new EmailAddress(email, "End USER");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, subject, message);
            return client.SendEmailAsync(msg);
        }

    }
}
