using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class ContextExtension
    {
        public static IQueryable<Context> WithActiveTabs(this IQueryable<Context> query) => query.Where(c => c.IsActive);
        public static IQueryable<Context> WithQrToken(this IQueryable<Context> query, string qrToken) => query.Where(c => c.QrToken == qrToken);
        public static IQueryable<Context> WithContextPart(this IQueryable<Context> query, string contextPartName)
            => query
            .Where(c => c.Parts != null && c.Parts
            .Any(p => p.Name == contextPartName));
        public static IQueryable<Context> WithActiveTabsAndContextPart(this IQueryable<Context> query, string contextPartName)
                => query
                .Include(c => c.Parts)
                .Where(c => c.IsActive && c.Parts != null && c.Parts
                .Any(p => p.Name == contextPartName));

        public static IQueryable<Context> Search(this IQueryable<Context> query, string searchTerm) => query
                .Where(c => c.Name.Contains(searchTerm)
                || c.QrToken.Contains(searchTerm)
                || c.QrToken.Contains(searchTerm)
                 || c.IsActive)
            .Take(10);
    }
}
