using Application.DTOs.PaymentFolder;
using Application.DTOs.TransactionFolder;
using Domain.Models;

namespace Application.DTOs.UserTabFolder.Resonse
{
    public record UserTabResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
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
        public IReadOnlyList<PaymentSummaryResponse>? Payments { get; init; }

        public static UserTabResponse FromUserTab(UserTab tab)
        {
            return new UserTabResponse
            {
                Id = tab.Id,
                UserId = tab.UserId,
                UserFullName = $"{tab.User?.FirstName} {tab.User?.LastName}".Trim(),
                ContextId = tab.ContextId,
                ContextName = tab.ContextPart?.Name ?? string.Empty,
                Status = tab.Status.ToString(),
                CreatedAt = tab.CreatedAt,
                ClosedAt = tab.ClosedAt,
            };
        }
    }
}
