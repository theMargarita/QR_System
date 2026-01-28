using Application.DTOs.Summary;

namespace Application.DTOs.Response
{
    public record UserProfileResponse
    {
        public int Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; init; }
        public DateTime CreatedAt { get; init; }

        // Aktiva tabs
        public List<UserTabSummaryResponse> ActiveTabs { get; init; } = new();

        // Senaste transaktioner
        public List<TransactionSummaryResponse> RecentTransactions { get; init; } = new();

        // Senaste betalningar
        public List<PaymentSummaryResponse> RecentPayments { get; init; } = new();

        // Statistik
        public decimal TotalSpent { get; init; }
        //public decimal CurrentOwed { get; init; }? // Kan läggas till om nödvändigt
    }
}
