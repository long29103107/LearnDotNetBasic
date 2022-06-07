using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TodoList.DB;
using TodoList.DTO;
using TodoList.Helpers;
using TodoList.Interfaces;

namespace TodoList.Services
{
    public class MailDailyService : IMailDailyService
    {
        private readonly IConfiguration _configuration;
        private readonly ITodoItemService _todoItemService;
        private readonly IUserService _userService;
        public MailDailyService(IConfiguration configuration
            , ITodoItemService todoItemService
            , IUserService userService
            )
        {
            _configuration = configuration;
            _todoItemService = todoItemService;
            _userService = userService;
        }
 
        public async Task<bool> SendMailDaily(string email)
        {
            try
            {
                string templateContent = File.ReadAllText(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "/Resources/HtmlTemplates/UserList.html");

                var userDTOs = await _userService.GetList();

                var contentReport = "";
                foreach (UserDTO user in userDTOs)
                {
                    contentReport += "<tr>";
                    contentReport += "<td style = 'border: 1px solid #dddddd; text-align: left; padding: 8px; '>" + user.UserName + "</td>";
                    contentReport += "<td style = 'border: 1px solid #dddddd; text-align: left; padding: 8px; '>" + user.Email + "</td>";
                    contentReport += "</tr>";
                }

                templateContent = templateContent.Replace("{{datatable}}", contentReport);
                templateContent = templateContent.Replace("{{username}}", "Long");
                SendGridEmailSender emailHelper = new SendGridEmailSender(_configuration);
                return await emailHelper.SendEmail(email, templateContent, "Report daily !");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
