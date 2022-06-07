using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.DB;
using TodoList.Interfaces;

namespace TodoList.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoContext _context;
        public UserRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetList() => await _context.Users.ToListAsync();
    }
}
