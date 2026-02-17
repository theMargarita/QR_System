using Domain.Models;

namespace Application.DTOs.UserTabFolder.Resonse
{
    //Get endpoint with userID 
    public class UserDetailTabResponses
    {
        public Guid Id { get; init; }
        public string? ContextPartName { get; init; } //ex. table 5
        public TabStatus Status { get; init; }

        //order list from dto
        public List<UserOrderResponse> UserOrder { get; init; } = new List<UserOrderResponse>();

        //user total
        public decimal UserTotal { get; init; }
        public decimal Paid { get; init; }
        public decimal Remaining { get; init; } //question is if this needs to be in this dto or the contextpart dto

        //other on the tab
        public int OtherParticipantCount { get; init; }
        public List<string>? OtherParticipantsName { get; init; } //maybe valid


        public static UserDetailTabResponses FromBody(UserTab tab)
        {
            var totalAmount = tab?.Transactions?.Sum(t => t.Total) ?? 0m;
            var totalPaid = tab?.Payments?.Sum(p => p.Amount) ?? 0m;
            var remaining = totalAmount - totalPaid;

            return new UserDetailTabResponses
            {
                Id = tab.Id,
                ContextPartName = tab.ContextPart.Name,
                Status = tab.Status,

                //user total
                UserTotal = tab.User.Tabs.Count(t => !t.IsClosed),
                Paid = totalPaid,
                Remaining = remaining,

                //nesten dtos
                UserOrder = tab.Transactions.Select(UserOrderResponse.FromBody).ToList(),
            };
        }
    }

    public class UserOrderResponse
    {
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset OrderedAt { get; set; }

        public static UserOrderResponse FromBody(Transaction transaction) 
        {
            return new UserOrderResponse
            {
                Id = transaction.Id,
                ProductName = transaction.Product.Name,
                Quantity = transaction.Quantity,
                Amount = transaction.Total,
                OrderedAt = transaction.CreatedAt
            };
        }
    }
}
