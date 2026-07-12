using NetApp.Application.DTOs;
using NetApp.Application.Interfaces;
using NetApp.Domain.Entities;
using NetApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category is null) return null;

            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                CreatedDate = category.CreatedDate,
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
                CreatedDate = DateTime.Now,
                CreatedBy = "Application"
            };

            await _categoryRepository.AddAsync(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(dto.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category {dto.Name} not found");

            if (dto.Name != null)
                category.Name = dto.Name;

            category.UpdatedDate = DateTime.Now;
            category.UpdatedBy = "Application";

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
