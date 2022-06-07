using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.DTO;
using TodoList.Authentication;
using TodoList.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;
        private readonly IRedisService _redisService;
        private const string cacheKey = "todoItems";

        public TodoItemsController(IRedisService redisService, ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
            _redisService = redisService;
        }

        // GET: api/TodoItems
        [HttpGet]
        //[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Member)]
        //[Authorize(Roles = "Admin,Member")] //var roleAdmin = HttpContext.User.IsInRole(UserRoles.Admin);
        //var roleMember = HttpContext.User.IsInRole(UserRoles.Member);
        public async Task<ActionResult<List<TodoItemDTO>>> GetTodoItems()
        {
           
            var todoItems = await _redisService.Get<List<TodoItemDTO>>(cacheKey);
            if (todoItems != null)
                return Ok(todoItems);

            todoItems = await _todoItemService.GetList();

            await _redisService.Set(cacheKey, todoItems);

            return Ok(todoItems);
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(long id)
        {
            var todoItem = await _todoItemService.Get(id);
            if (todoItem == null)
                return Ok(new Response { Status = "Error", Message = "Not Found!" });
            return todoItem;
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoItemDTO)
        {
            var result = await _todoItemService.Post(todoItemDTO);

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var result = await _todoItemService.Put(id, todoItemDTO);
            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // PATCH: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchTodoItem(long id, TodoItemDTO todoItemDTO)
        {
            var result = await _todoItemService.Patch(id, todoItemDTO);

            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var result = await _todoItemService.Delete(id);

            if(result)
                await _redisService.Delete(cacheKey);

            return Ok(result);
        }
    }
}
