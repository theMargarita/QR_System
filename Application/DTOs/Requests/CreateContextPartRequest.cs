using Domain.Models;

namespace Application.DTOs.Requests
{
    // create new context part (ex borddel)
    public record CreateContextPartRequest
    {
        public string Name { get; init; } = string.Empty;
        public int ContextId { get; init; } // which context (bord) this part belongs to
        public int TabId { get; set; }
        public int UserId { get; set; }

        //these can stay here incase 
        public DateTime JoinedAt { get; set; }
        public DateTime LeftAt { get; set; }
    }
}
