using Domain.Models;

namespace Application.DTOs.UserTabFolder.Resonse
{
    //Get endpoint with userID 
    public class UserDetailTabResponses
    {
        public Guid Id { get; set; }
        public string? ContextPartName { get; set; } //ex. table 5
        public TabStatus Status { get; set; }

        //order list from dto
        public List<UserOrderReponse> UserOrder { get; set; } = new List<UserOrderReponse>();

        //user total
        public decimal UserTotal { get; set; }
        public decimal Paid { get; set; }
        public decimal Remaining { get; set; } //question is if this needs to be in this dto or the contextpart dto

        //other on the tab
        public int OtherParticipantCount { get; set; }
        public List<string>? OtherParticipantsName { get; set; } //maybe valid
    }

    public class UserOrderReponse
    {
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Amout { get; set; }
        public DateTimeOffset OrderedAt { get; set; }
    }
}
