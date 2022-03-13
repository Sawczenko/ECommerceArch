using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Infrastructure
{
    public class MailSender : IMailSender
    {
        private readonly SendGridClient _sendGridClient;

        public MailSender(IConfiguration configuration)
        {
            _sendGridClient = new SendGridClient(configuration["SendGridApiKey"]);
        }
        public async Task Send(Email email)
        {
            var to = new EmailAddress(email.To, email.To);
            var from = new EmailAddress(email.From, email.From);
            var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Content, email.Content);
            var response = await _sendGridClient.SendEmailAsync(message);
        }
    }
}
