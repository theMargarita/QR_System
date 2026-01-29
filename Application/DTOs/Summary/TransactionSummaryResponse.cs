namespace Application.DTOs.Summary
{
    public record TransactionSummaryResponse
    {
        public int Id { get; init; }
        public string ItemName { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal UnitPrice { get; init; }
        public decimal Total { get; init; }
        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
    }
}