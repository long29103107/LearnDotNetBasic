using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DB;
using TodoList.Entities;
using TodoList.Interfaces;


namespace TodoList.Repositories
{
    public class TodoItemDapperRepository : ITodoItemDapperRepository
    {
        private readonly DapperContext _context;
        public TodoItemDapperRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetList()
        {
            var query = $"SELECT * FROM TodoItems";

            using (IDbConnection conn = _context.CreateConnection())
            {
                return (await conn.QueryAsync<TodoItem>(sql: query)).ToList();
            }
        }

        public async Task<TodoItem> Get(long id)
        {
            var query = $"SELECT * FROM TodoItems WHERE Id = {id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<TodoItem>(sql: query);
            }
        }

        public async Task<TodoItem> Post(TodoItem todoItem)
        {
            var query = $"INSERT INTO TodoItems (Name, IsComplete, Secret) VALUES (N'{todoItem.Name}', {Convert.ToInt32(todoItem.IsComplete)}, N'{todoItem.Secret}') " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection conn = _context.CreateConnection())
            {
                var id = await conn.QuerySingleAsync<int>(sql: query);
                var createdTodoItem = new TodoItem
                {
                    Id = id,
                    Name = todoItem.Name,
                    IsComplete = todoItem.IsComplete,
                    Secret = todoItem.Secret
                };
                return createdTodoItem;
            }
        }

        public async Task<TodoItem> Put(long id, TodoItem todoItem)
        {
            todoItem.Id = id;
            var query = $"UPDATE TodoItems SET Name = N'{todoItem.Name}', IsComplete = {Convert.ToInt32(todoItem.IsComplete)}, Secret = N'{todoItem.Secret}' WHERE Id = {id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return todoItem;
                return null;
            }
        }

        public async Task<TodoItem> Patch(TodoItem todoItem)
        {
            var query = $"UPDATE TodoItems SET Name = N'{todoItem.Name}', IsComplete = {Convert.ToInt32(todoItem.IsComplete)}, Secret = N'{todoItem.Secret}' WHERE Id = {todoItem.Id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return todoItem;
                return null;
            }
        }

        public async Task<bool> Delete(long id)
        {
            var query = $"DELETE FROM TodoItems WHERE Id = {id}";
            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return true;
                return false;
            }
        }
    }
}
