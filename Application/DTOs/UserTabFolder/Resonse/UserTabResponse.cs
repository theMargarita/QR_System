using Application.DTOs.PaymentFolder;
using Application.DTOs.TransactionFolder;

namespace Application.DTOs.UserTabFolder.Resonse
{
    public class UserTabResponse
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string UserFullName { get; init; } = string.Empty;
        public int ContextId { get; init; }
        public string ContextName { get; init; } = string.Empty;
        public string Status { get; init; } = string.Empty;
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? ClosedAt { get; init; }
        public decimal TotalAmount { get; init; }
        public decimal PaidAmount { get; init; }
        public decimal RemainingAmount { get; init; }
        public bool IsPaid { get; init; }
        public IReadOnlyList<TransactionSummaryResponse>? Transactions { get; init; }
        public IReadOnlyList<PaymentSummaryResponse>? Payments{ get; init; }
    }
}
