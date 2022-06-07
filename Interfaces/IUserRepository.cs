using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;

namespace TodoList.Interfaces
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetList();
    }
}
