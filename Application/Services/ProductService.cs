using Application.DTOs.ProductFolder;
using Application.IServices;
using Domain.IRepositories;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;
        public ProductService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            var item = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Status = request.Status
            };

            await _unitOfWork.Products.CreateAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return new ProductResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Category = item.Category,
                Status = item.Status
            };
        }

        public async Task DeleteProductAsync(int id)
        {
            var item = await _unitOfWork.Products.GetByIdAsync(id);
            if (item == null)
            {
                _logger.LogWarning("Attempted to delete non-existent product with ID {ProductId}", id);
                return;
            }

            await _unitOfWork.Products.DeleteAsync(item);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Product now deleted with ID {id}");
        }

        public async Task<IEnumerable<ProductResponse>> GetActiveProductsAsync()
        {
            var products = await _unitOfWork.Products.GetActiveProductsAsync();
            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                Status = p.Status
            });
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var items = await _unitOfWork.Products.GetAllAsync();

            return items.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Category = p.Category,
                Status = p.Status
            });
        }

        public async Task<ProductResponse> GetProductByIdAsync(int id)
        {
            var item = await _unitOfWork.Products.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning($"Product with ID {id} not found");
                return null;
            }

            return new ProductResponse
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Category = item.Category,
                Status = item.Status
            };
        }

        public async Task UpdateProductAsync(int id, ProductRequest request)
        {
            var item = await _unitOfWork.Products.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning($"Attempted to update non-existent product with ID {id}");
                return;
            }

            var updatedItem = await _unitOfWork.Products.UpdateAsync(new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Category = request.Category,
                Status = request.Status
            });

            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"Product with ID {id} updated successfully");

            //not sure about this
             new ProductResponse
            {
                Id = updatedItem.Id,
                Name = updatedItem.Name,
                Description = updatedItem.Description,
                Price = updatedItem.Price,
                Category = updatedItem.Category,
                Status = updatedItem.Status
            };
        }
    }
}
