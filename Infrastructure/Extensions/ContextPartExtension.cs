using Domian.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class ContextPartExtension
    {
        public static IQueryable<ContextPart> QrCode(this IQueryable<ContextPart> query, string qrToken)
        {
            return query.Include(cp => cp.Context)
                .Include(cp => cp.UserTabs)
                .Where(x => x.QrToken == qrToken)
                .Where(cp => cp.IsActive);
        }

        public static IQueryable<ContextPart> CheckTables(this IQueryable<ContextPart> query, int contextId)
        {
            return query.Where(cp => cp.IsActive && cp.UserTabs
                .Any(ut => !ut.IsClosed))
                .Include(cp => cp.UserTabs
                .Where(ut => !ut.IsClosed));
        }

        //public static IQueryable<ContextPart> WithContext(this IQueryable<ContextPart> query, Guid cpId)
        //{
        //    return query.Where(cp => cp.Id == cpId)
        //       .Select(cp => cp.UserTabs)
        //       .Where(ut => !ut.)
        //       .Select(ut => ut.UserId)
        //       .Distinct()
        //       .CountAsync().Result > 0;
        //}

        //public static IQueryable<ContextPart> UserHasActiveTabAsync(this IQueryable<ContextPart> query, Guid contextPartId, Guid userId)
        //{
        //    return query
        //      .Where(cp => cp.Id == contextPartId)
        //      .Include(cp => cp.UserTabs)
        //      .Select(cp => cp.UserTabs).

        //}

        //public static IQueryable<ContextPart> DashboardDetail(this IQueryable<ContextPart> query)
        //{
        //    return query.Include(cp => cp.UserTabs).Include(cp => cp.trans)
        //}
    }
}
