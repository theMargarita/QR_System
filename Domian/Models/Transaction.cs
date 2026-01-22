namespace Domain.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal PriceEach { get; set; }
        public decimal Total => Quantity * PriceEach;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


        //navigation property - not sure if needed
        public Product? Product { get; set; }
        public int ProductId { get; set;  }
        public UserTab ? Tab { get; set; }
        public int TabId { get; set; }

        public User? User { get; set; }
        public int UserId { get; set; }

    }
}
