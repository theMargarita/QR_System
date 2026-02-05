using Domain.Models;

namespace Application.DTOs.UserTabFolder
{
    public record UserTabSummaryResponse
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public TabStatus Status { get; init; } //enum
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; init; }
        public decimal PaidAmount { get; init; }
        public decimal RemainingAmount { get; init; }
        public bool IsPaid { get; init; }
        public int TransactionCount { get; init; }
    }
}
