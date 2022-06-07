using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;

namespace TodoList.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetList();
    }
}
