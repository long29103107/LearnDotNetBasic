using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DB;
using TodoList.Entities;
using TodoList.Interfaces;
using TodoList.Request;
using System.Linq.Dynamic.Core;
using TodoList.Helpers;
using TodoList.DTO;
using AutoMapper;

namespace TodoList.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TodoContext _context;
        private readonly IMapper _mapper;
        public ProductRepository(TodoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Product>> GetList(ProductRequest request)
        {
            var orderField = "Id";
            var orderValue = "asc";
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                orderField = request.OrderBy.Split(":")[0];
                orderValue = request.OrderBy.Split(":")[1];
            }
            return await (from products in _context.Products.Where(p =>
                                            (string.IsNullOrEmpty(request.Name) || p.Name.Contains(request.Name)) &&
                                            (string.IsNullOrEmpty(request.Sku) || p.Sku.Contains(request.Sku)) &&
                                            ((request.CategoryId == null || request.CategoryId == 0) || p.CategoryId == request.CategoryId) &&
                                            ((request.MinPrice == null || request.MinPrice == 0) || request.MinPrice <= p.Price) &&
                                            (request.CreatedAtFrom == DateTime.MinValue || request.CreatedAtFrom <= p.CreatedAt) &&
                                            (request.CreatedAtTo == DateTime.MaxValue || request.CreatedAtTo.AddTicks(-1).AddDays(1) >= p.CreatedAt) &&
                                            (request.UpdatedAtFrom == DateTime.MinValue || request.UpdatedAtFrom <= p.UpdatedAt) &&
                                            (request.UpdatedAtTo == DateTime.MaxValue || request.UpdatedAtTo.AddTicks(-1).AddDays(1) >= p.UpdatedAt))
                                        .Include(p => p.Category)
                                        .OrderBy($"{orderField} {orderValue}")
                          join categories in _context.Categories on products.CategoryId equals categories.Id
                          select new Product
                          {
                              Id = products.Id,
                              Name = products.Name,
                              Sku = products.Sku,
                              CategoryId = products.CategoryId,
                              Description = products.Description,
                              Price = products.Price,
                              Quantity = products.Quantity,
                              Category = products.Category,
                              CreatedAt = products.CreatedAt,
                              CreatedBy = products.CreatedBy,
                              UpdatedAt = products.UpdatedAt,
                              UpdatedBy = products.UpdatedBy
                          })
                            .Skip((request.Page - 1) * request.Size).Take(request.Size).ToListAsync();
        }


        public async Task<Product> Get(long id) => await _context.Products.FindAsync(id);
        public async Task<Product> Post(ProductDTO productDTO)
        {
            var authenHelper = new AuthenticationHelper();
            //var product = _mapper.Map<Product>(productDTO);
            var product = new Product();
            product.Sku = productDTO.Sku;
            product.CategoryId = productDTO.CategoryId;
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price ?? 0;
            product.Quantity = productDTO.Quantity ?? 0;

            product.CreatedAt = DateTime.Parse(DateTime.Now.ToString());
            product.CreatedBy = await authenHelper.GetUserId();

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Put(long id, ProductDTO productDTO)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (product == null)
                return null;

            var authenHelper = new AuthenticationHelper();
            product.UpdatedAt = DateTime.Parse(DateTime.Now.ToString());
            product.UpdatedBy = await authenHelper.GetUserId();
            product.Sku = productDTO.Sku;
            product.CategoryId = productDTO.CategoryId;
            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price ?? 0;
            product.Quantity = productDTO.Quantity ?? 0;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return product;
        }

        public async Task<Product> Patch(long id, ProductDTO productDTO)
        {            
            var authenHelper = new AuthenticationHelper();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return null;

            //oldProduct = _mapper.Map(productDTO, oldProduct);

            product.Sku = productDTO.Sku;
            product.CategoryId = productDTO.CategoryId;
            product.Name ??= productDTO.Name;
            product.Description ??= productDTO.Description;
            product.Price = productDTO.Price == null ? product.Price : Convert.ToDouble(productDTO.Price);
            product.Quantity = productDTO.Quantity == null ? product.Quantity : Convert.ToInt32(productDTO.Quantity);

            product.UpdatedAt = DateTime.Parse(DateTime.Now.ToString());
            product.UpdatedBy = await authenHelper.GetUserId();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
            return product;
        }

        public async Task<bool> Delete(long id)
        {
            var product = await this.Get(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
