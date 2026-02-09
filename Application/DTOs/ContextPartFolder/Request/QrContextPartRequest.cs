using Domain.Models;

namespace Application.DTOs.CPFolder.Request
{
    // create new context part (ex borddel)
    public record QrContextPartRequest //should be used for usertab?
    {
        public string Name { get; init; } = string.Empty;
        public int ContextId { get; init; } // which context (bord) this part belongs to

        //en användare ska kunna scanna en qr kod men en anväändare behöver inte vara med på skapandet av qr koden
        public Guid UserId { get; set; }
        //gäller desamma till usertabid maybe? 
        public int UserTabId { get; set; }
        public bool IsActive { get; set; } = true;

        //these can stay here incase 
        public DateTime JoinedAt { get; set; }
        public DateTime LeftAt { get; set; }
    }
}
