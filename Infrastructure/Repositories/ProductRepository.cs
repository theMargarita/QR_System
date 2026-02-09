using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(QrDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
           return await _dbSet.Where(p => p.Status == ProductStatus.Active).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _dbSet.Where(p => p.Category != null).ToListAsync();
        }

        public async Task<IEnumerable<Product>> SearchProduct(string searchTerm)
        {
            return await _dbSet
                .Where(p => p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm) ||
                            p.Category!.Contains(searchTerm))
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        //public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId)
        //{

        //}

        //public Task<Product?> GetProductWithCategoryAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
