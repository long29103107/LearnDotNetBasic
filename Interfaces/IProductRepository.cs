using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Request;

namespace TodoList.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetList(ProductRequest request );
        Task<Product> Get(long id);
        Task<Product> Post(ProductDTO productDTO);
        Task<Product> Put(long id, ProductDTO product);
        Task<Product> Patch(long id, ProductDTO productDTO);
        Task<bool> Delete(long id);
    }
}
