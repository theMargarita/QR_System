using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.UserFolder.Request
{
    public record CreateUserRequest //DTO for creating or updating a User
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]

        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 50 characters")]
        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = new DateTimeOffset(DateTime.Now);
    }
}
