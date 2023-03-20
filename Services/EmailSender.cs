using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ReenbitCamp_TestTask_azure_func.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _sendGridClient;

        public EmailSender(string sendGridApiKey)
        {
            _sendGridClient = new SendGridClient(sendGridApiKey);
        }

        public async Task SendNotificationEmailAsync(string fromEmailAddress, string toEmailAddress, string templateId, Dictionary<string, string> dynamicTemplateData)
        {
            var from = new EmailAddress(fromEmailAddress);
            var to = new EmailAddress(toEmailAddress);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);

            var response = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
