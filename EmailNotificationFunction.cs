using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Blob;
using ReenbitCamp_TestTask_azure_func.Services;
using SendGrid.Helpers.Mail;

namespace ReenbitCamp_TestTask_azure_func
{
    [StorageAccount("BlobConnectionString")]
    public class EmailNotificationFunction
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSenderService;

        public EmailNotificationFunction(IConfiguration configuration, IEmailSender emailSender)
        {
            _configuration = configuration;
            _emailSenderService = emailSender;
        }
        [FunctionName("EmailNotificationFunction")]
        public async Task Run([BlobTrigger("docxfiles/{name}")]Stream myBlob, IDictionary<string, string> metaData, string name, ILogger log)
        {
            log.LogInformation($"Trigger triggered email notification function started its work");

            try
            {
                if (metaData.TryGetValue("emailAddress", out string toEmailAddress))
                {

                    var dynamicTemplateData = new Dictionary<string, string>
                    {
                        { "toEmailAddress", toEmailAddress },
                        { "fileName", name },
                        { "fileExtension", Path.GetExtension(name) },
                        { "dateTime", DateTime.Now.ToString()}
                    };

                    await _emailSenderService.SendNotificationEmailAsync(_configuration["SenderEmail"], toEmailAddress, _configuration["TemplateId"], dynamicTemplateData);

                    log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
                }
                else
                {
                    log.LogInformation($"Email address was not provided. Notification was not delivered");
                }

            }
            catch (Exception ex)
            {
                log.LogError("Sending email failed", ex);
            }
        }
    }
}
