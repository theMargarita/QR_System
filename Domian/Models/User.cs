using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public int? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } 

        //navigatio propery
        public ICollection<UserTab>? Tabs { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Payment>? Payments { get; set; }



        //+46708562851
        //+46 70 856 28 51
        //(+46)708562851
        //0708562851
    }
}
