using Domian.Models;

namespace Domain.Models
{
    public class UserTab //användar nota
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ContextId { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset ClosedAt { get; set; } = DateTimeOffset.UtcNow; 
        public TabStatus Status { get; set; }

        public int? OwnerId { get; set; }
        public int? ContextPartId { get; set; }

        //navgation property
        public ContextPart? ContextPart { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Payment>? Payments { get; set; }

        //not fully sure about this one
        public User? User { get; set; }


        public bool IsPaid() => Payments != null && Transactions != null &&
            Payments.Sum(p => p.Amount) >= Transactions.Sum(t => t.Total);
    }
    public enum TabStatus
    {
        Open = 1,
        Paid = 2,
        Closed = 3,
    }



    //order (gruppering)
    //- orderRows (Transactions)
    //- payments ( Payments)
}
