using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Phone]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$")]
        public string? PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } 

        //navigatio propery
        public ICollection<UserTab>? Tabs { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Payment>? Payments { get; set; }


        //string pattern = @"^([\+]?61[-]?|0)?[1-9][0-9]{8}$";   // AU example
        //bool ok = Regex.IsMatch(input, pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

        //+46708562851
        //+46 70 856 28 51
        //(+46)708562851
        //0708562851
    }
}
