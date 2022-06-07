using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.DTO;
using TodoList.Entities;
using TodoList.Interfaces;

namespace TodoList.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _cateRepo;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository cateRepo, IMapper mapper)
        {
            _cateRepo = cateRepo;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetList() => _mapper.Map<List<CategoryDTO>>(await _cateRepo.GetList());

        public async Task<CategoryDTO> Get(long id) => _mapper.Map<CategoryDTO>(await _cateRepo.Get(id));

        public async Task<CategoryDTO> Post(CategoryDTO categoryDTO)
        {
            var newCategory = await _cateRepo.Post(_mapper.Map<Category>(categoryDTO));
            return _mapper.Map<CategoryDTO>(newCategory);
        }

        public async Task<CategoryDTO> Put(long id, CategoryDTO categoryDTO)
        {
            var newCategory = await _cateRepo.Put(id, _mapper.Map<Category>(categoryDTO));
            return _mapper.Map<CategoryDTO>(newCategory);
        }
        public async Task<CategoryDTO> Patch(long id, CategoryDTO categoryDTO)
        {
            var entity = await _cateRepo.Get(id);
            if (entity == null)
                return null;

            var newEntity = _mapper.Map(categoryDTO, entity);
            newEntity.Id = id;

            return _mapper.Map<CategoryDTO>(await _cateRepo.Patch(newEntity));
        }

        public async Task<bool> Delete(long id)
        {
            return await _cateRepo.Delete(id);
        }
    }
}
