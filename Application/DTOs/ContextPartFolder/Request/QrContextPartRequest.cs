using Domain.Models;

namespace Application.DTOs.CPFolder.Request
{
    // create new context part (ex borddel)
    public record QrContextPartRequest 
    {
        public string Name { get; init; } = string.Empty;
        public Guid ContextId { get; init; } 

        public Guid UserId { get; set; }
        //gäller desamma till usertabid maybe? 
        public Guid UserTabId { get; set; }
        public bool IsActive { get; set; } = true;

        //these can stay here incase 
        public DateTime JoinedAt { get; set; }
        public DateTime LeftAt { get; set; }
    }
}
