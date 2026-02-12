using Domain.Models;

namespace Application.DTOs.UserTabFolder.AdminTabFolder
{
    //admin tab for order - a get endpoint with tabid
    public class TabWithOrdersSummary
    {
        public int TabId { get; set; }
        public string? ContextName { get; set; }
        public TabStatus Status { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        // Alla beställningar grupperade per användare
        public List<UserOrdersSummary>? UserOrders { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalPaid { get; set; }
    }
    public class UserOrdersSummary
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public List<OrderLineSummary>? Orders { get; set; }
        public decimal SubTotal { get; set; }
    }

    public class OrderLineSummary
    {
        public int TransactionId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Total { get; set; }
        public DateTimeOffset OrderedAt { get; set; }
    }

}
