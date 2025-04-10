using AutoMapper;
using ExpenseTracker.Data;
using ExpenseTracker.DTOs;
using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _repository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetCategoryByIdAsync(id);
            return category == null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.AddCategoryAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _repository.UpdateCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(int id, CategoryUpdateDto categoryUpdateDto)
        {
            // Get existing category or create a new one if not found
            var category = await _repository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                category = new Category { Id = id };
            }

            // Update properties from DTO
            category.Name = categoryUpdateDto.Name;
            category.Description = categoryUpdateDto.Description;

            // Save changes
            await _repository.UpdateCategoryAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _repository.DeleteCategoryAsync(id);
        }

        public async Task<bool> CategoryExistsAsync(int id)
        {
            return await _repository.CategoryExistsAsync(id);
        }
    }
} 