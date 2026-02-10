using Domain.Models;

namespace Application.DTOs.ProductFolder
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public ProductStatus Status { get; set; }  = ProductStatus.Active;


        public static ProductResponse FromProduct(Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Status = product.Status
            };
        }
    }
}
