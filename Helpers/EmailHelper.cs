using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using TodoList.Authentication;

namespace TodoList.Helpers
{
    public class EmailHelper
    {
        private readonly IConfiguration _configuration;
        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool SendEmail(string userEmail, string bodyHtml, string subject)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_configuration["EmailConfiguration:From"]);
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = subject;
            mailMessage.Body = bodyHtml;
            mailMessage.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(_configuration["EmailConfiguration:Username"], _configuration["EmailConfiguration:Password"]);
            client.Host = _configuration["EmailConfiguration:SmtpServer"];
            client.EnableSsl = true;
            client.Port = int.Parse(_configuration["EmailConfiguration:Port"]);

            try
            {
                client.Send(mailMessage);
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
