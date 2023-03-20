using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenbitCamp_TestTask_azure_func.Services
{
    public interface IEmailSender
    {
        public Task SendNotificationEmailAsync(string fromEmailAddress, string toEmailAddress, string templateId, Dictionary<string, string> dynamicTemplateData);
    }
}
