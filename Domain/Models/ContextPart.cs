using Domain.Models;

namespace Domain.Models
{
    public class ContextPart //ex, sven svensson - eller bord 11
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; //ex. bord 11
        public string ? QrToken { get; set; }
        public bool IsActive { get; set; } = true;

        public Context? Context { get; set; }

        public ICollection<UserTab>? UserTabs { get; set; } //användare som är i denna contextpart
    }
}
