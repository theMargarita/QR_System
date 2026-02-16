using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserFolder.Request
{
    public record CreateUserRequest //DTO for creating or updating a User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = new DateTimeOffset(DateTime.Now);
    }
}
