using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace BonusSystem.Configuration
{
    public class Configuration
    {
        public Configuration()
        {

            var dir = Directory.GetParent(Directory.GetCurrentDirectory())
                .CreateSubdirectory("BonusSystem.Configuration");
            if (!dir.Exists) throw new AccessViolationException("Can not access Configuration path");
            string projectPath = dir.FullName;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile("appsettings.json")
                    .Build();


            EnterpriseDbContext = configuration.GetConnectionString("EnterpriseDbContext");

            SmtpClientHost = configuration.GetSection("MailConfiguration:SmtpClientHost").Value;
            SmtpClientPort = configuration.GetSection("MailConfiguration:SmtpClientPort").Value;
            SmtpClientEnableSsl = configuration.GetSection("MailConfiguration:SmtpClientEnableSsl").Value;
            SmtpClientCredentialUser = configuration.GetSection("MailConfiguration:SmtpClientCredentialUser").Value;
            SmtpClientCredentialPassword = configuration.GetSection("MailConfiguration:SmtpClientCredentialPassword").Value;
            SmtpClientAdminEmail = configuration.GetSection("MailConfiguration:SmtpClientAdminEmail").Value;
            SmtpClientRedirectEmail = configuration.GetSection("MailConfiguration:SmtpClientRedirectEmail").Value;

            ElasticSearchUrl = configuration.GetSection("ElasticSearchConfiguration:ElasticSearchUrl").Value;
            ElasticSearchLogin = configuration.GetSection("ElasticSearchConfiguration:ElasticSearchLogin").Value;
            ElasticSearchPassword = configuration.GetSection("ElasticSearchConfiguration:ElasticSearchPassword").Value;
            ElasticSearchEnvirment = configuration.GetSection("ElasticSearchConfiguration:ElasticSearchDABEnvirment").Value;
            ElasticSearchCheck = configuration.GetSection("ElasticSearchConfiguration:ElasticSearchCheck").Value;

        }


        public string EnterpriseDBConnection { get; set; }
        public string EnterpriseDbContext { get; set; }
        public string EnterpriseDbReportConnection { get; set; }

        [Url]
        public string SmtpClientHost { get; set; }
        public string SmtpClientPort { get; set; }
        public string SmtpClientEnableSsl { get; set; }
        public string SmtpClientCredentialUser { get; set; }
        public string SmtpClientCredentialPassword { get; set; }
        public string SmtpClientRedirectEmail { get; set; }
        public string SmtpClientAdminEmail { get; set; }

        [Url]
        public string ElasticSearchUrl { get; set; }
        public string ElasticSearchLogin { get; set; }
        public string ElasticSearchPassword { get; set; }
        public string ElasticSearchEnvirment { get; set; }
        public string ElasticSearchCheck { get; set; }
    }
}
