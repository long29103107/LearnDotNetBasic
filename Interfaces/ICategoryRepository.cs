using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Entities;

namespace TodoList.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetList();
        Task<Category> Get(long id);
        Task<Category> Post(Category category);
        Task<Category> Put(long id, Category category);
        Task<Category> Patch(Category category);
        Task<bool> Delete(long id);
    }
}
