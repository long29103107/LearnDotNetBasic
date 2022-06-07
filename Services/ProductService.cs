using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Interfaces;
using TodoList.Request;

namespace TodoList.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _proRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository proRepo, IMapper mapper)
        {
            _proRepo = proRepo;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetList(ProductRequest request) => _mapper.Map<List<ProductDTO>>(await _proRepo.GetList(request));

        public async Task<ProductDTO> Get(long id) => _mapper.Map<ProductDTO>(await _proRepo.Get(id));

        public async Task<ProductDTO> Post(ProductDTO productDTO)
        {
            var newProduct = await _proRepo.Post(productDTO);
            return _mapper.Map<ProductDTO>(newProduct);
        }

        public async Task<ProductDTO> Put(long id, ProductDTO productDTO)
        {
            var newProduct = await _proRepo.Put(id, productDTO);
            return _mapper.Map<ProductDTO>(newProduct);
        }
        public async Task<ProductDTO> Patch(long id, ProductDTO productDTO)
        {
            var newTodoItem = await _proRepo.Patch(id, productDTO);
            return _mapper.Map<ProductDTO>(newTodoItem);
        }

        public async Task<bool> Delete(long id)
        {
            return await _proRepo.Delete(id);
        }
    }
}
