using Domain.Models;

namespace Domian.Models
{
    public class ContextPart //ex, sven svensson - eller bord 11
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Context? Context { get; set; }

        public ICollection<UserTab>? UserTabs { get; set; }
    }
}
