using Application.DTOs.PaymentFolder;
using Application.DTOs.TransactionFolder;
using Domain.Models;

namespace Application.DTOs.UserTabFolder
{
    public record UserTabResponse
    {
        public int Id { get; init; }
        public int UserId { get; init; }
        public string UserFullName { get; init; } = string.Empty;
        public int ContextId { get; init; }
        public string ContextName { get; init; } = string.Empty; // ex bordsnamn

        public TabStatus Status { get; init; } // enum
        public string StatusDisplay => Status.ToString(); // "Open", "Paid", "Closed"

        public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? ClosedAt { get; init; } = DateTimeOffset.UtcNow;

        public decimal TotalAmount { get; init; } // summan av alla transaktioner på de "bordet"
        public decimal PaidAmount { get; init; } // summan av alla betalningar som personer betalt på de "bordet"
        public decimal RemainingAmount { get; init; } // TotalAmount - PaidAmount

        //public bool IsPaid { get; init; } // om PaidAmount >= TotalAmount
        public bool IsPaid => PaidAmount >= TotalAmount; 

        // Nested collections heter de 
        public IReadOnlyList<TransactionSummaryResponse>? Transactions { get; init; } 
        public IReadOnlyList<PaymentSummaryResponse>? Payments { get; init; } 
    }
}

