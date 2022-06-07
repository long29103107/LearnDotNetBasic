using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Request;

namespace TodoList.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetList(ProductRequest request);
        Task<ProductDTO> Get(long id); 
        Task<ProductDTO> Post(ProductDTO productDTO);
        Task<ProductDTO> Put(long id, ProductDTO productDTO);
        Task<ProductDTO> Patch(long id, ProductDTO productDTO);
        Task<bool> Delete(long id);
    }
}
