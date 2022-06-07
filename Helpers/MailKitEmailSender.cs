using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace TodoList.Helpers
{
    public class MailKitEmailSender
    {
        private readonly IConfiguration _configuration;

        public MailKitEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string userEmail, string bodyEmail, string subject)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(_configuration["EmailConfiguration:FromAdressTitle"], _configuration["EmailConfiguration:FromAdress"]));
                mimeMessage.To.Add(new MailboxAddress(_configuration["EmailConfiguration:ToAdressTitle"], _configuration["EmailConfiguration:ToAdress"]));
                mimeMessage.Subject = subject;

                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = bodyEmail;
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                //mimeMessage.Body = new TextPart("plain")
                //{
                //    Text = bodyEmail

                //};

                using (var client = new SmtpClient())
                {

                    client.Connect(_configuration["EmailConfiguration:SmtpServer"], int.Parse(_configuration["EmailConfiguration:Port"]), false);
                    client.Authenticate(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                }
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
            return false;
        }
    }
}
