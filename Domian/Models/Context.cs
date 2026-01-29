using Domian.Models;

namespace Domain.Models
{
    public class Context //representerar en restaurang/cafe/bar/bord/etc
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //ölkyl

        public Owner? Owner { get; set; } //DC
        public int? OwnerId { get; set; }

        public bool ContextPartIsUnique { get; set; } //om true, kan en användare bara vara i en contextpart i taget inom denna context

        public ICollection<ContextPart>? Parts { get; set; }
    }

    public class Owner //digital creation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
