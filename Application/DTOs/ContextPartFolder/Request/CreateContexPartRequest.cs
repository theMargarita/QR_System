using Domain.Models;

namespace Application.DTOs.ContextPartFolder.Request
{
    public record CreateContexPartRequest
    {
        public string Name { get; set; } = string.Empty; 
        public string? QrToken { get; set; }
        public bool IsActive { get; set; } = true;
        public Guid ContextId { get; set; }
    }
}
