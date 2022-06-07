using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Interfaces;

namespace TodoList.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemDapperRepository _todoItemRepo;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemDapperRepository todoItemRepo, IMapper mapper)
        {
            _todoItemRepo = todoItemRepo;
            _mapper = mapper;
        }

        public async Task<List<TodoItemDTO>> GetList() => _mapper.Map<List<TodoItemDTO>>(await _todoItemRepo.GetList());
    
        public async Task<TodoItemDTO> Get(long id) => _mapper.Map<TodoItemDTO>(await _todoItemRepo.Get(id));

        public async Task<TodoItemDTO> Post(TodoItemDTO todoItemDTO)
        {
            var newTodoItem = await _todoItemRepo.Post(_mapper.Map<TodoItem>(todoItemDTO));
            return _mapper.Map<TodoItemDTO>(newTodoItem);
        }

        public async Task<TodoItemDTO> Put(long id, TodoItemDTO todoItemDTO)
        {
            var newTodoItem = await _todoItemRepo.Put(id, _mapper.Map<TodoItem>(todoItemDTO));
            return _mapper.Map<TodoItemDTO>(newTodoItem);
        }
        public async Task<TodoItemDTO> Patch(long id, TodoItemDTO todoItemDTO)
        {
            var entity = await _todoItemRepo.Get(id);
            if (entity == null)
                return null;
            var newTodoItem = await _todoItemRepo.Patch(_mapper.Map(todoItemDTO, entity));
            return _mapper.Map<TodoItemDTO>(newTodoItem);
        }

        public async Task<bool> Delete(long id)
        {
            return await _todoItemRepo.Delete(id);
        }
    }
}
