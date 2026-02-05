using Application.DTOs.UserFolder.Response;

namespace Application.DTOs.PaymentFolder
{
    public record PaymentSummaryResponse
    {
        public int Id { get; init; }
        public decimal Amount { get; init; }
        //public int UserTabId { get; init; }
        public string PaymentMethod { get; init; } = string.Empty;
        public DateTimeOffset PaidAt { get; init; } = DateTimeOffset.UtcNow;

        // Optional: Include user information if needed
        public IReadOnlyList<UserResponse>? Users { get; init; } 
    }
}