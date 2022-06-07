using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Interfaces;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _cateService;
        private readonly IRedisService _redisService;
        private const string cacheKey = "categories";
        public CategoryController(IRedisService redisService, ICategoryService cateService)
        {
            _redisService = redisService;
            _cateService = cateService;
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var catetegories = await _redisService.Get<List<CategoryDTO>>(cacheKey);
            if (catetegories != null)
                return Ok(catetegories);

            catetegories = await _cateService.GetList();

            await _redisService.Set(cacheKey, catetegories);

            return Ok(catetegories);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(long id)
        {
            var category = await _cateService.Get(id);
            if (category == null)
                return Ok(new Response { Status = "Error", Message = "Not Found!" });
            return category;
        }

        // POST: api/category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
        {
            var result = await _cateService.Post(categoryDTO);

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // PUT: api/category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, CategoryDTO categoryDTO)
        {
            var result = await _cateService.Put(id, categoryDTO);
            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // PATCH: api/category/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory(long id, CategoryDTO categoryDTO)
        {
            var result = await _cateService.Patch(id, categoryDTO);

            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _cateService.Delete(id);

            if (result)
                await _redisService.Delete(cacheKey);

            return Ok(result);
        }
    }
}
