using EmailService.Interfaces;
using EmailService.Models;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System;
using System.Diagnostics;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using BonusSystem.Configuration.ServiceConfigs;

namespace EmailService
{
    public class EmailManager : IEmailManager
    {
        private readonly EmailServiceParameters _mailParameters = new EmailServiceParameters();
        private readonly ILogger _logger;

        public EmailManager(ILogger<EmailManager> logger)
        {
            _logger = logger;

        }

        /// <summary>
        /// Send notification email for clients
        /// </summary>
        public async Task SendEmailAsync(EmailParams emailparams)
        {

            if (emailparams.UserList.Count == 0)
            {
                return;
            }
            foreach (var emailparam in emailparams.UserList)
            {
                var emailMessage = new MimeMessage();

                if (emailparam.UserEmail == null)
                {
                    emailparam.UserEmail = _mailParameters.SmtpRedirectEmail;
                }

                emailMessage.From.Add(new MailboxAddress("Компанія \"TEST\" ", _mailParameters.SmtpUser));

                emailMessage.Subject = emailparams.Title;

                var builder = new BodyBuilder();

                builder.HtmlBody =
                    (EmailFace.Up
                    + "<h4> Шановний(на)" + emailparam.UserFullName + "!</h4><br>"
                    + emailparams.Message
                    + EmailFace.Down);

                emailMessage.Body = builder.ToMessageBody();

                var isRedirectConfigured = !string.IsNullOrWhiteSpace(_mailParameters.SmtpRedirectEmail);
                emailMessage.To.Add(isRedirectConfigured ? new MailboxAddress("", _mailParameters.SmtpRedirectEmail) :
                    new MailboxAddress("", emailparam.UserEmail));

                try
                {
                    using (var client = new SmtpClient())
                    {
                        client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        await client.ConnectAsync(_mailParameters.SmtpHost, _mailParameters.SmtpPort, SecureSocketOptions.Auto);
                        await client.AuthenticateAsync(_mailParameters.SmtpUser, _mailParameters.SmtpPassword);
                        await client.SendAsync(emailMessage);
                        await client.DisconnectAsync(true);
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    var stack = (new StackTrace(ex, true)).GetFrame(0);
                    _logger.LogError($"\n\n\n Exeption in File : {stack.GetFileName()} ; \n\n\n Line : {stack.GetFileLineNumber()} ; \n\n\n Message : {ex.Message} \n\n\n");
                }
            }
        }


    }
}
