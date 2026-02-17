using Domain.Models;

namespace Application.DTOs.UserTabFolder.Request
{
    public record OrderRequest
    {
        public Guid UserId { get; init; }
        public Guid ContextPartId { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset ClosedAt { get; init; }
        public TabStatus Status { get; init; }

        //cuz is it not the plan to order? 
        //public ICollection<Product>? Products { get; set; }

        public static OrderRequest FromBody(UserTab tab)
        {
            return new OrderRequest
            {

            };
        }
    }
}
