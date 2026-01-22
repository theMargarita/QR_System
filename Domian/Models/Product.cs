using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; } = string.Empty;
        public ProductStatus Status { get; set; }  = ProductStatus.Active;
    }
    public enum ProductStatus
    {
        Active,
        Inactive
    }
}
