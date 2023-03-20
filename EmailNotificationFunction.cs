using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
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
        private readonly IEmailSender _emailSenderService;

        public EmailNotificationFunction(IEmailSender emailSender)
        {
            _emailSenderService = emailSender;
        }
        [FunctionName("EmailNotificationFunction")]
        public void Run([BlobTrigger("docxfiles/{name}")]Stream myBlob, IDictionary<string, string> metaData, string name, ILogger log)
        {
            log.LogInformation($"Trigger triggered email notification function started its work");

            try
            {
                string toEmailAddress = metaData.ContainsKey("emailAddress") ? metaData["emailAddress"] : null;

                if (toEmailAddress != null)
                {
                    var subject = "File uploaded successfully!";
                    var plainTextContent = $"The file {name} has been uploaded successfully to the blob container.";
                    var htmlContent = $"The file <strong>{name}</strong> has been uploaded successfully to the blob container.";

                    _emailSenderService.SendNotificationEmail("zefirova.sofiia@gmail.com", toEmailAddress, subject, plainTextContent, htmlContent);

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
