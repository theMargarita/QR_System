using Application.DTOs.ProductFolder;

namespace Application.IServices
{
    public interface IProductService
    {
        Task<ProductResponse> CreateProductAsync(ProductRequest request);
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<IEnumerable<ProductResponse>> GetActiveProductsAsync();
        Task UpdateProductAsync(int id, ProductRequest request);
        Task DeleteProductAsync(int id);
    }
}
