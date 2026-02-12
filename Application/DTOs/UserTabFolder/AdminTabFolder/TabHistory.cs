using Domain.Models;

namespace Application.DTOs.UserTabFolder.AdminTabFolder
{
    //history for admin
    public class TabHistory
    {
        public int Id { get; set; }
        public string? ContextName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ClosedAt { get; set; }
        public TimeSpan Duration { get; set; }

        //
        public int ParticipantCount { get; set; }
        public int TotalTransaction { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }

        public TabStatus FinalStatus { get; set; }
    }
}
