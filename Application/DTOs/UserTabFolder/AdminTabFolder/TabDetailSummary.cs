using Domain.Models;

namespace Application.DTOs.UserTabFolder.AdminTabFolder
{
    //also for admin - get endpoint with tabid
    public class TabDetailSummary
    {
        public int TabId { get; set; }
        public int ContextId { get; set; }
        public string? ContextName { get; set; }
        public string? ContextType { get; set; }  // "table" eller "event"
        public TabStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ClosedAt { get; set; }

        // Deltagare med deras skulder
        public List<ParticipantDebtSummary>? Participants { get; set; }

        // Totaler
        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingAmount { get; set; }
        public bool IsFullyPaid { get; set; }
    }

    public class ParticipantDebtSummary
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal TotalOrdered { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal RemainingDebt { get; set; }
        public bool IsFullyPaid { get; set; }
        public DateTimeOffset JoinedAt { get; set; }
    }
}
