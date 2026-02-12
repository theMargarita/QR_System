namespace Domain.Models
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public PaymentMethod Method { get; set; } 
        public string? Note { get; set; } //som kvitto?

        //navigation property
        public UserTab? Tab { get; set; }
        public Guid? TabId { get; set; }

        public User? User { get; set; }
        public Guid? UserId { get; set; }
    }


    public enum PaymentMethod
    {
        Cash = 1,
        Swish = 2,
    }
}
