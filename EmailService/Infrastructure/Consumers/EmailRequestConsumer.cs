using System;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using MassTransit;

namespace Infrastructure
{
    public class EmailRequestConsumer : IConsumer<Email>
    {
        private readonly IMailSender _mailSender;

        public EmailRequestConsumer(IMailSender mailSender)
        {
            _mailSender = mailSender;
        }

        public async Task Consume(ConsumeContext<Email> context)
        {
            await _mailSender.Send((Email) context.Message);
        }
    }
}