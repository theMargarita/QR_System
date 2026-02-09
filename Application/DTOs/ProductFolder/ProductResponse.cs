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
    }
}
