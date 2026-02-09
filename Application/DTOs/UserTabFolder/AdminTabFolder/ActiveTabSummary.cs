using Domain.Models;

namespace Application.DTOs.UserTabFolder.AdminTabFolder
{
    //this is for admin - this is for a get endpoint 
    public record ActiveTabSummary
    {
        public int TabId { get; set; }
        public string? ContextPartName { get; set; }  // "Bord 5"
        public int ParticipantCount { get; set; }

        // Totals
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingAmount { get; set; }

        // Status
        public TabStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
