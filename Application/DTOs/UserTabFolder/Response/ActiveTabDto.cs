using Domain.Models;

namespace Application.DTOs.UserTabFolder.Resonse
{
    public class ActiveTabDto
    {
        //get endpoint for qrtoken
        public Guid Id { get; set; } 
        public Guid ContextId { get; set; }
        public string? ContextName { get; set; }
        //public string? ContextType { get; set; } // ex. "Table", "Room", "Event"?
        public TabStatus Status { get; set; }
        public bool IsActive { get; set; }
        public int CurrentParticipantCount { get; set; }
    }
}
