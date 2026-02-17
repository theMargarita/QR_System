using Application.DTOs.PaymentFolder;
using Application.DTOs.TransactionFolder;
using Domain.Models;

namespace Application.DTOs.UserTabFolder.Response
{
    public record UserTabResponse
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string UserFullName { get; init; } = string.Empty;
        public Guid PartId { get; init; }
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

        public static UserTabResponse FromBody(UserTab tab)
        {
            var totalAmount = tab.Transactions?.Sum(t => t.Total) ?? 0;
            var totalPaid = tab.Payments?.Sum(p => p.Amount) ?? 0;

            return new UserTabResponse
            {
                Id = tab.Id,
                UserId = tab.UserId,
                UserFullName = tab.User != null ? $"{tab.User?.FirstName} {tab.User?.LastName}".Trim() : null,
                PartId = tab.ContextPartId.Value,
                ContextName = tab.ContextPart != null ? tab.ContextPart?.Name ?? string.Empty : null,
                Status = tab.Status.ToString(),
                CreatedAt = tab.CreatedAt,
                ClosedAt = tab.ClosedAt,

                //calculations
                TotalAmount = totalAmount,
                PaidAmount = totalPaid,
                RemainingAmount = totalAmount - totalPaid,
                IsPaid = totalPaid >= totalAmount && totalAmount > 0,

                //nested dtos
                Transactions = tab.Transactions?.Select(TransactionSummaryResponse.FromBody).ToList(),
                Payments = tab.Payments?.Select(PaymentSummaryResponse.FromBody).ToList()
            };
        }
    }
}
