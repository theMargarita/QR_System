using Application.DTOs.UserFolder.Response;
using Domain.Models;

namespace Application.DTOs.PaymentFolder
{
    public record PaymentSummaryResponse
    {
        public Guid Id { get; init; }
        public decimal Amount { get; init; }
        public PaymentMethod? Method { get; init; } 
        public DateTimeOffset PaidAt { get; init; } 

        public static PaymentSummaryResponse FromBody(Payment payment)
        {
            return new PaymentSummaryResponse
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Method = payment.Method,
                PaidAt = payment.CreatedAt,
            };
        }
    }
}