using Domain.Models;

namespace Application.DTOs.TransactionFolder
{
    public record TransactionSummaryResponse
    {
        public Guid Id { get; init; }
        public string? ProductName { get; init; } 
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Total { get; init; }
        public DateTimeOffset CreatedAt { get; init; } 

        public static TransactionSummaryResponse FromBody(Transaction transaction)
        {
            return new TransactionSummaryResponse
            {
                Id = transaction.Id,
                ProductName = transaction.Product.Name,
                Quantity = transaction.Quantity,
                CreatedAt = transaction.CreatedAt,
                Total = transaction.Total,
                UnitPrice = transaction.PriceEach
            };

        }
    }
}