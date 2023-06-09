﻿using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReenbitCamp_TestTask_azure_func.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(ReenbitCamp_TestTask_azure_func.Startup))]
namespace ReenbitCamp_TestTask_azure_func
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();

            builder.Services.AddSingleton<IEmailSender, EmailSender>(s =>
                new EmailSender(config["SendGridApiKey"]));
        }
    }
}
