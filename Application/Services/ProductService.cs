using Application.DTOs.ProductFolder;
using Application.Helpers;
using Application.IServices;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly QrDbContext _context;
        private readonly ILogger _logger;
        public ProductService(ILogger<ProductService> logger, QrDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Status = request.Status
            };

            _context.Products.Add(product);

            await _context.SaveChangesAsync();

            return ProductResponse.FromProduct(product);


        }

        public async Task DeleteProductAsync(int id)
        {
            var query = await _context.Products.FindAsync(id);

            if (query == null)
            {
                _logger.LogWarning($"Attempted to delete non-existent product with ID {id}");
                return;
            }

            _context.Products.Remove(query);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Product now deleted with ID {id}");
        }

        public async Task<IEnumerable<ProductResponse>> GetActiveProductsAsync() => await _context.Products
            .ActiveProducts()
            .Select(x => ProductResponse.FromProduct(x))
            .ToListAsync();

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {

            return await _context.Products
                .Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    Category = p.Category,
                    Status = p.Status
                }).ToListAsync();
        }

        //vet faktiskt inte om denna är relevant men kan ha kvar den ändå
        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var item = await _context.Products.FindAsync(id);

            if (item == null)
            {
                _logger.LogWarning($"Product with ID {id} not found");
                return null;
            }

            return ProductResponse.FromProduct(item);
        }

        public async Task UpdateProductAsync(int id, ProductRequest request)
        {
            var item = await _context.Products.FindAsync(id);

            if (item == null)
            {
                _logger.LogWarning($"Attempted to update non-existent product with ID {id}");
                return;
            }

            var updatedItem = _context.Products.Update(new Product
            {
                Id = id,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Status = request.Status
            });

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Product with ID {id} updated successfully");
        }
        public async Task<IEnumerable<ProductResponse>> SearchProductAsync(string searchTerm)
        {
            StringHelper.Normalize(searchTerm);

            if (searchTerm == null || string.IsNullOrWhiteSpace(searchTerm))
            {
                _logger.LogWarning($"Search term is invalid: {searchTerm}");
                return null;
            }

            return await _context.Products
                 .SearchProduct(searchTerm)
                 .Select(p => ProductResponse.FromProduct(p))
                 .ToListAsync();
        }
    }
}
