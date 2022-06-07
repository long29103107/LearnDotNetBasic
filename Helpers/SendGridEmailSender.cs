using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace TodoList.Helpers
{
    public class SendGridEmailSender
    {
        private readonly IConfiguration _configuration;
        public SendGridEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmail(string userEmail, string bodyEmail, string subject)
        {
            try
            {
                //Use when Set sendgrid_api_key in environmentvariable
                //var apikey = environment.getenvironmentvariable("sendgrid_api_key");
                //var client = new sendgridclient(apikey);

                //Use config 
                var client = new SendGridClient(_configuration["Sendgrid:ApiKeyFromSendGrid"]);
                var from = new EmailAddress(_configuration["Sendgrid:FromAddress"], _configuration["Sendgrid:FromAddressTitle"]);

                var to = new EmailAddress(userEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, null , bodyEmail);
                var response = await client.SendEmailAsync(msg);
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
