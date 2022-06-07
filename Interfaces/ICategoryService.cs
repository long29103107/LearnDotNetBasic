using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;

namespace TodoList.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetList();
        Task<CategoryDTO> Get(long id);
        Task<CategoryDTO> Post(CategoryDTO categoryDTO);
        Task<CategoryDTO> Put(long id, CategoryDTO categoryDTO);
        Task<CategoryDTO> Patch(long id, CategoryDTO categoryDTO);
        Task<bool> Delete(long id);
    }
}
