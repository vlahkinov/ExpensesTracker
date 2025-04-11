using AutoMapper;
using ExpenseTracker.Models;
using ExpenseTracker.DTOs;

namespace ExpenseTracker.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>();
            
            // Expense mappings
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : null))
                .ReverseMap()
                .ForMember(dest => dest.Category, opt => opt.Ignore());
                
            CreateMap<ExpenseCreateDto, Expense>().ReverseMap();
            CreateMap<ExpenseUpdateDto, Expense>().ReverseMap();
        }
    }
} 