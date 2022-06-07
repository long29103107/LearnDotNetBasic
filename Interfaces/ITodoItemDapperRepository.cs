using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Entities;

namespace TodoList.Interfaces
{
    public interface ITodoItemDapperRepository
    {
        Task<List<TodoItem>> GetList();
        Task<TodoItem> Get(long id);
        Task<TodoItem> Post(TodoItem todoItem);
        Task<TodoItem> Put(long id, TodoItem todoItem);
        Task<TodoItem> Patch(TodoItem todoItem);
        Task<bool> Delete(long id);
    }
}
