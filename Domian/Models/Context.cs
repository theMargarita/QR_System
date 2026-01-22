namespace Domain.Models
{
    public class Context //representerar en restaurang/cafe/bar/bord/etc
    {
        public int Id { get; set; }
        public string Name { get; set; } //ölkyl

        public Owner? Owner { get; set; }
        public int? OwnerId { get; set; }

        public bool ContextPartIsUnique { get; set; }

        public ICollection<ContextPart>? Parts { get; set; }
    }

    public class Owner //digital creation
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class ContextPart //ex, pontus bremdal - eller bord 11
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Context Context { get; set; }

        public ICollection<UserTab>? UserTabs { get; set; }
    }
}
