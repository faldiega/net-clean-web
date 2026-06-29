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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            });
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null) return null;

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CreatedAt = product.CreatedAt,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name ?? string.Empty
            };
        }

        public async Task<IEnumerable<ProductDto>> GetByCategoryAsync(int categoryId)
        {
            var products = await _productRepository.GetByCategoryAsync(categoryId);
            return products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CreatedAt = p.CreatedAt,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name ?? string.Empty
            });
        }

        public async Task CreateAsync(CreateProductDto dto)
        {
            var exists = await _productRepository.ExistsByNameAsync(dto.Name);
            if (exists)
                throw new InvalidOperationException($"Product '{dto.Name}' already exists.");

            var product = new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.Now,
                CreatedBy = "Application"
            };

            await _productRepository.AddAsync(product);
        }

        public async Task UpdateAsync(UpdateProductDto dto)
        {
            var exists = await _productRepository.ExistsByNameAsync(dto.Name);
            if (exists)
                throw new InvalidOperationException($"Product '{dto.Name}' already exists.");

            var product = new Product
            {
                ProductId = dto.ProductId,
                Name = dto.Name,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            await _productRepository.UpdateAsync(product);
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _productRepository.ExistsByNameAsync(name);
        }
    }


}
