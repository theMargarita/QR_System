using Domain.Models;

namespace Application.DTOs.UserTabFolder.Resonse
{
    public class ActiveTabDto
    {
        //get endpoint for qrtoken
        public int Id { get; set; } //dont know if this one should be a guid or a int
        public Guid ContextId { get; set; }
        public string? ContextName { get; set; }
        public string? ContextType { get; set; } // ex. "Table", "Room", "Event"?
        public TabStatus Status { get; set; }
        public bool IsActive { get; set; }
        public int CurrentParticipantCount { get; set; }
    }
}
