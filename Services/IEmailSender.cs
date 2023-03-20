using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReenbitCamp_TestTask_azure_func.Services
{
    public interface IEmailSender
    {
        public void SendNotificationEmail(string fromEmailAddress, string toEmailAddress, string subject, string plainTextContent, string htmlContent);
    }
}
