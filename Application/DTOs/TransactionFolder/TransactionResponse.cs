using Application.DTOs.ProductFolder;
using Application.DTOs.UserFolder.Response;
using Application.DTOs.UserTabFolder.Resonse;
using Domain.Models;

namespace Application.DTOs.TransactionFolder
{
    public record TransactionResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; } //antal produkt
        public decimal PriceEach { get; set; } //per vara
        public decimal Total => Quantity * PriceEach;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public ProductResponse? Product { get; set; }
        public int ProductId { get; set; }
        public UserTabResponse? Tab { get; set; }
        public Guid TabId { get; set; }
        public UserResponse? User { get; set; }
        public Guid UserId { get; set; }

        public static TransactionResponse FromTransaction(Transaction transaction)
        {
            return new TransactionResponse
            {
                Id = transaction.Id, // Assuming Id is a Guid, you might want to convert it to int or keep it as Guid
                Quantity = transaction.Quantity,
                PriceEach = transaction.PriceEach,
                CreatedAt = transaction.CreatedAt,
                Product = transaction.Product != null ? ProductResponse.FromProduct(transaction.Product) : null,
                ProductId = transaction.ProductId,
                Tab = transaction.Tab != null ? UserTabResponse.FromUserTab(transaction.Tab) : null,
                TabId = transaction.TabId,
                User = transaction.User != null ? UserResponse.FromUser(transaction.User) : null,
                UserId = transaction.UserId
            };
        }
    }
}  
