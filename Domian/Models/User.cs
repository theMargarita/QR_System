using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        //public int Id { get; set; }
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; }
        //public int TaleId { get; set; } //ContextPart?
        public DateTime CreatedAt { get; set; } 

        //navigatio propery - many to many
        public ICollection<UserTab>? Tabs { get; set; } // är användar nota ? 
        public ICollection<Transaction>? Transactions { get; set; } //
        public ICollection<Payment>? Payments { get; set; } //hur det betalas

        //+46708562851
        //+46 70 856 28 51
        //(+46)708562851
        //0708562851
    }
}
