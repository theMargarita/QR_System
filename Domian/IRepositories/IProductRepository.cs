using Domain.Models;

namespace Domain.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        //category does not exist at the moment
        //Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> SearchProduct(string searchTerm);
        //Task<Product?> GetProductWithCategoryAsync(int id);
    }
}
