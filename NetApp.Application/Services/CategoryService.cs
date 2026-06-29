using NetApp.Application.DTOs;
using NetApp.Application.Interfaces;
using NetApp.Domain.Entities;
using NetApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetApp.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                CreatedAt = c.CreatedAt,
                ProductCount = c.Products.Count
            });
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                ProductCount = category.Products.Count
            };
        }

        public async Task CreateAsync(CreateCategoryDto dto)
        {
            var exists = await _categoryRepository.ExistsByNameAsync(dto.Name);
            if (exists)
                throw new InvalidOperationException($"Category '{dto.Name}' already exists.");

            var category = new Category
            {
                Name = dto.Name,
                CreatedAt = DateTime.Now,
                CreatedBy = "Application"
            };

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var exists = await _categoryRepository.ExistsByNameAsync(dto.Name);
            if (exists)
                throw new InvalidOperationException($"Category '{dto.Name}' already exists.");

            var category = new Category
            {
                CategoryId = dto.CategoryId,
                Name = dto.Name
            };

            await _categoryRepository.UpdateAsync(category);
        }

        public async Task DeleteAsync(int id)
        {
            await _categoryRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _categoryRepository.ExistsByNameAsync(name);
        }
    }


}
