using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Interfaces;
using TodoList.DTO;

namespace TodoList.Jobs
{
    public class JobDaily
    {
        private readonly IMailDailyService _mailDailyService;
        private readonly ITodoItemService _todoItemService;
        public JobDaily(IMailDailyService mailDailyService, ITodoItemService todoItemService)
        {
            _mailDailyService = mailDailyService;
            _todoItemService = todoItemService;
        }

        public async Task<bool> SendMailDaily(string email)
        {
            return await _mailDailyService.SendMailDaily(email);
        }
        public async Task<TodoItemDTO> CreateTodoItem()
        {
            TodoItemDTO todoItemDTO = new TodoItemDTO
            {
                Name = "Item " + Guid.NewGuid().ToString(),
                Secret = "Secret" + Guid.NewGuid().ToString(),
            };
            
            return await _todoItemService.Post(todoItemDTO);
        }
    }
}
