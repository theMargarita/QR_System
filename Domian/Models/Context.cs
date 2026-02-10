using Domian.Models;

namespace Domain.Models
{
    public class Context //representerar en restaurang/cafe/bar/bord/etc
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; //ölkyl
        public string? QrToken { get; set; }
        public bool IsActive { get; set; } //om false, är context inaktiv och kan inte användas för då använder någon den förmodar jag 

        public Owner? Owner { get; set; } //DC
        public Guid? OwnerId { get; set; }

        public bool ContextPartIsUnique { get; set; } //om true, kan en användare bara vara i en contextpart i taget inom denna context

        public ICollection<ContextPart>? Parts { get; set; }
    }

    public class Owner //digital creation
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
