using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;

namespace TodoList.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItemDTO>> GetList();
        Task<TodoItemDTO> Get(long id);
        Task<TodoItemDTO> Post(TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> Put(long id, TodoItemDTO todoItemDTO);
        Task<TodoItemDTO> Patch(long id, TodoItemDTO todoItemDTO);
        Task<bool> Delete(long id);
    }
}
