using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoList.Interfaces
{
    public interface IMailDailyService
    {
        Task<bool> SendMailDaily(string email);
    }
}
