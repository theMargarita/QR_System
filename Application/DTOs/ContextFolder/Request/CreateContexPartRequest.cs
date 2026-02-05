using Domain.Models;

namespace Application.DTOs.ContextFolder.Request
{
    public record CreateContexPartRequest
    {
        public string Name { get; set; } = string.Empty; 
        public string? QrToken { get; set; }
        public bool IsActive { get; set; } = true;
        public int ContextId { get; set; }
    }
}
