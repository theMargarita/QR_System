namespace Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public PaymentMethod Method { get; set; } //som kvitto?
        public string? Note { get; set; } //som kvitto?

        //navigation property
        public UserTab? Tab { get; set; }
        public int? TabId { get; set; }

        public User? User { get; set; }
        public int? UserId { get; set; }
    }


    public enum PaymentMethod
    {
        Cash = 1,
        Swish = 2,
    }
}
