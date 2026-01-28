namespace Application.DTOs.Requests
{
    public record CreateUserRequest //DTO for creating or updating a User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public IReadOnlyList<UserTabResponse>? Tabs { get; set; }
        public IReadOnlyList<TransactionReponse>? Transactions { get; set; }
        public IReadOnlyList<PaymentResonse>? Payments { get; set; }

    }
}
