using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.DTO;
using TodoList.Entities;

namespace TodoList
{
    public class TodoProfile : Profile
    {
        public TodoProfile ()
        {
            CreateMap<TodoItemDTO, TodoItem>()
                 .ForMember(dest => dest.Id, src => src.Ignore())
                 .ForMember(dest => dest.Secret, opt => opt.Condition(src => (src.Secret != null)))
                 .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                 .ForMember(dest => dest.IsComplete, opt => opt.Condition(src => (src.IsComplete != null)))
                 .ReverseMap();
            CreateMap<UserDTO, ApplicationUser>()
                 .ReverseMap();
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                .ReverseMap();
            CreateMap<ProductDTO, Product>()
                .ForMember(dest => dest.Id, src => src.Ignore())
                .ForMember(dest => dest.CreatedAt, src => src.Ignore())
                .ForMember(dest => dest.CreatedBy, src => src.Ignore())
                .ForMember(dest => dest.UpdatedAt, src => src.Ignore())
                .ForMember(dest => dest.UpdatedBy, src => src.Ignore())
                .ForMember(dest => dest.Sku, opt => opt.Condition(src => src.Sku != null))
                .ForMember(dest => dest.CategoryId, opt => opt.Condition(src => (src.CategoryId != null)))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => (src.Name != null)))
                .ForMember(dest => dest.Description, opt => opt.Condition(src => (src.Description != null)))
                .ForMember(dest => dest.Price, opt => opt.Condition(src => (src.Price != null)))
                .ForMember(dest => dest.Quantity, opt => opt.Condition(src => (src.Quantity != null)))
                .ReverseMap();
        }
    }
}
