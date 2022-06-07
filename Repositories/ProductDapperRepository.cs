using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DB;
using TodoList.Entities;
using TodoList.Interfaces;
using TodoList.Request;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Http;
using TodoList.Helpers;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace TodoList.Repositories
{
    public class ProductDapperRepository //: IProductRepository
    {
        private readonly DapperContext _context;
        private readonly ICategoryRepository _cateRepo;
        public ProductDapperRepository(DapperContext context, ICategoryRepository cateRepo)
        {
            _context = context;
            _cateRepo = cateRepo;
        }

        public async Task<List<Product>> GetList(ProductRequest request)
        {
            var page = request.Page;
            var size = request.Size;
            //var search = request.search;
            var orderField = "Id";
            var orderValue = "asc";
            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                orderField = request.OrderBy.Split(":")[0];
                orderValue = request.OrderBy.Split(":")[1];
            }

            var query = $" SELECT P.*, C.* " +
                $" FROM Products P  INNER JOIN Categories C ON P.CategoryId = C.Id ";

            //if (!string.IsNullOrEmpty(search))
            //{
            //    query += $" WHERE P.Name LIKE N'%{search}%' " +
            //        $" OR P.Sku LIKE N'%{search}%' " +
            //        $" OR P.Price LIKE N'%{search}%' " +
            //        $" OR P.Description LIKE N'%{search}%' " +
            //        $" OR C.Name LIKE N'%{search}%' ";
            //}

            query += $" ORDER BY P.{orderField} {orderValue} " +
                $" OFFSET {(page - 1) * size} ROWS FETCH NEXT {size} ROWS ONLY";

            using (IDbConnection conn = _context.CreateConnection())
            {
                var products = conn.Query<Product, Category, Product>
                    (sql: query, map: (product, category) => { product.Category = category; return product; });
                return products.ToList();
            }
        }

        public async Task<Product> Get(long id)
        {
            var query = $"SELECT P.*, C.* from Products P  INNER JOIN Categories C ON P.CategoryId = C.Id Where P.Id = {id}";
            using (IDbConnection conn = _context.CreateConnection())
            {
                var product = conn.Query<Product, Category, Product>
                    (sql: query, map: (product, category) => { product.Category = category; return product; });

                return product.FirstOrDefault();
            }
        }

        public async Task<Product> Post(Product product)
        {
            var authenHelper = new AuthenticationHelper();
            var userId = await authenHelper.GetUserId();
            var createdAt = DateTime.Parse(DateTime.Now.ToString());

            var query = $"INSERT INTO Products(Sku, CategoryId, Name, Description, Price, Quantity, CreatedAt, CreatedBy)" +
                            $"VALUES (N'{product.Sku}', {product.CategoryId}, N'{product.Name}', N'{product.Description}', {product.Price}, {product.Quantity}, '{createdAt}', '{userId}')" +
                            $"SELECT CAST(SCOPE_IDENTITY() as int)";

            using (IDbConnection conn = _context.CreateConnection())
            {
                var id = await conn.QuerySingleAsync<int>(sql: query);
                var createdProduct = new Product
                {
                    Id = id,
                    Sku = product.Sku,
                    CategoryId = product.CategoryId,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = await _cateRepo.Get(product.CategoryId),
                    CreatedBy = userId,
                    CreatedAt = createdAt,   
                    UpdatedAt = product.UpdatedAt,
                    UpdatedBy = product.UpdatedBy
                };
                return createdProduct;
            }
        }

        public async Task<Product> Put(long id, Product product)
        {
            product.Id = id;

            var oldProduct = await this.Get(id);
            if(oldProduct == null)
                return null;

            product.CreatedAt = oldProduct.CreatedAt;
            product.CreatedBy = oldProduct.CreatedBy;

            var authenHelper = new AuthenticationHelper();
            product.UpdatedBy = await authenHelper.GetUserId();

            product.UpdatedAt = DateTime.Parse(DateTime.Now.ToString());

            var query = $"UPDATE Products " +
                $"SET Sku = @Sku, CategoryId = @CategoryId, Name = @Name, Description = @Description, Price = @Price, " +
                $"Quantity = @Quantity, UpdatedAt = @UpdatedAt, UpdatedBy = @UpdatedBy " +
                $"WHERE Id = {id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query, param: product) > 0)
                {
                    product.Category = await _cateRepo.Get(product.CategoryId);
                    return product;
                }
                return null;
            }
        }

        public async Task<Product> Patch(Product product)
        {
            var oldProduct = new Product();
            oldProduct = await this.Get(product.Id);
            if (oldProduct == null)
                return null;

            if (product.CategoryId == 0)
                product.CategoryId = oldProduct.CategoryId;

            var authenHelper = new AuthenticationHelper();

            product.CreatedAt = oldProduct.CreatedAt;
            product.CreatedBy = oldProduct.CreatedBy;
            product.UpdatedBy = await authenHelper.GetUserId();
            product.UpdatedAt = DateTime.Parse(DateTime.Now.ToString());

            var query = @"UPDATE Products
                        SET Sku = @Sku, CategoryId = @CategoryId, Name = @Name, Description = @Description, Price = @Price,
                        Quantity = @Quantity, UpdatedBy=@UpdatedBy, UpdatedAt=@UpdatedAt 
                        WHERE Id = @Id";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query, param: product) > 0)
                {
                    product.Category = await _cateRepo.Get(product.CategoryId);
                    return product;
                }
                return null;
            }
        }

        public async Task<bool> Delete(long id)
        {
            var query = $"DELETE FROM Products WHERE Id = {id}";
            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return true;
                return false;
            }
        }
    }
}
