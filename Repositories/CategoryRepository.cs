using Dapper;
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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DapperContext _context;
        public CategoryRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> GetList()
        {
            var query = @"SELECT * FROM Categories";

            using (IDbConnection conn = _context.CreateConnection())
            {
                return (await conn.QueryAsync<Category>(sql: query)).ToList();
            }
        }

        public async Task<Category> Get(long id)
        {
            var query = $"SELECT * FROM Categories where Id = {id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                return await conn.QueryFirstOrDefaultAsync<Category>(sql: query);
            }
        }

        public async Task<Category> Post(Category category)
        {
            var query = $"INSERT INTO Categories(Name) VALUES (N'{category.Name}')" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
            using (IDbConnection conn = _context.CreateConnection())
            {
                var id = await conn.QuerySingleAsync<int>(sql: query);
                var createdCategory = new Category
                {
                    Id = id,
                    Name = category.Name,
                };
                return createdCategory;
            }
        }

        public async Task<Category> Put(long id, Category category)
        {
            category.Id = id;
            var query = $"UPDATE Categories SET Name = N'{category.Name}' WHERE Id = {category.Id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return category;
                return null;
            }
        }

        public async Task<Category> Patch(Category category)
        {
            var query = $"UPDATE Categories SET Name =  N'{category.Name}' WHERE Id = {category.Id}";

            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return category;
                return null;
            }
        }

        public async Task<bool> Delete(long id)
        {
            var query = $"DELETE FROM Categories WHERE Id = {id}";
            using (IDbConnection conn = _context.CreateConnection())
            {
                if (await conn.ExecuteAsync(sql: query) > 0)
                    return true;
                return false;
            }
        }
    }
}
