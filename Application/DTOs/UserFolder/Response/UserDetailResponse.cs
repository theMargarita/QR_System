using Application.DTOs.TransactionFolder;
using Application.DTOs.UserTabFolder.Resonse;

namespace Application.DTOs.UserFolder.Response
{
    public record UserDetailResponse
    {
        public int Id { get; set; } //honestly not about this one but sure why not
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; init; }
        public bool HasPaid { get; set; } //istället för en hel lista med payments

        //navigation property isch
        public IReadOnlyList<UserTabResponse> ? Tabs { get; init; }
        public IReadOnlyList<TransactionResponse>? Transactions { get; init; }
    }
}
