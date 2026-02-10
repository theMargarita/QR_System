namespace Domain.Models
{
    public class Transaction //kanske sak hete kvitto eller liknande 
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Quantity { get; set; } //antal produkt
        public decimal PriceEach { get; set; } //per vara
        public decimal Total => Quantity * PriceEach;
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;


        public Product? Product { get; set; }
        public int ProductId { get; set;  }

        public UserTab ? Tab { get; set; }
        public Guid TabId { get; set; }

        public User? User { get; set; }
        public Guid UserId { get; set; }

    }
}
