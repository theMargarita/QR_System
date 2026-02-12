using Domain.Models;

namespace Application.DTOs.TransactionFolder
{
    public record TransactionRequest
    {
        public int Quantity { get; set; } //antal produkt
        public decimal PriceEach { get; set; } //per vara
        public decimal Total => Quantity * PriceEach;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        public Product? Product { get; set; }
        public int ProductId { get; set; }

        public UserTab? Tab { get; set; }
        public Guid TabId { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }

        public static Transaction ToTransaction(TransactionRequest request)
        {
            return new Transaction
            {
                Quantity = request.Quantity,
                PriceEach = request.PriceEach,
                CreatedAt = request.CreatedAt,
                ProductId = request.ProductId,
                TabId = request.TabId,
                UserId = request.UserId
            };
        }
    }
}
