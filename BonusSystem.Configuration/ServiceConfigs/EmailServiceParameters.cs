using System;
using System.Collections.Generic;
using System.Text;

namespace BonusSystem.Configuration.ServiceConfigs
{
    public class EmailServiceParameters
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpRedirectEmail { get; set; }

        public EmailServiceParameters()
        {
            var app = new Configuration();

            SmtpHost = app.SmtpClientHost;
            SmtpPort = int.Parse(app.SmtpClientPort);
            SmtpEnableSsl = bool.Parse(app.SmtpClientEnableSsl);
            SmtpUser = app.SmtpClientCredentialUser;
            SmtpPassword = app.SmtpClientCredentialPassword;
            SmtpRedirectEmail = app.SmtpClientRedirectEmail;
        }
    }
}
