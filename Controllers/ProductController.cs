using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.DTO;
using TodoList.Helpers;
using TodoList.Interfaces;
using TodoList.Request;

namespace TodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Admin + "," + UserRoles.Member)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _proService;
        private readonly IRedisService _redisService;
        private const string cacheKey = "products";
        public ProductController(IRedisService redisService, IProductService proService)
        {
            _redisService = redisService;
            _proService = proService;
        }

        // GET: api/product
        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts([FromQuery] ProductRequest request)
        {
            //var catetegories = await _redisService.Get<List<CategoryDTO>>(cacheKey);
            //if (catetegories != null)
            //    return Ok(catetegories);

            var products = await _proService.GetList(request);

            //await _redisService.Set(cacheKey, catetegories);

            return Ok(products);  
        }

        // GET: api/product/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(long id)
        {
            var product = await _proService.Get(id);

            if (product == null)
                return Ok(new Response { Status = "Error", Message = "Not Found!" });
            return product;
        }

        // POST: api/product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDTO)
        {
            var result = await _proService.Post(productDTO);

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // PUT: api/product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(long id, ProductDTO productDTO)
        {
            var result = await _proService.Put(id, productDTO);
            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }
        // PATCH: api/product/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchCategory(long id, ProductDTO productDTO)
        {
            var result = await _proService.Patch(id, productDTO);

            if (result == null)
                return Ok(new Response { Status = "Error", Message = "Update fail!" });

            await _redisService.Delete(cacheKey);

            return Ok(result);
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var result = await _proService.Delete(id);

            if (result)
                await _redisService.Delete(cacheKey);

            return Ok(result);
        }
    }
}
