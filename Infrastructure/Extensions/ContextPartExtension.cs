using Domain.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class ContextPartExtension
    {
        public static IQueryable<ContextPart> QrCode(this IQueryable<ContextPart> query, string qrToken)
        {
            return query.Include(cp => cp.Context)
                .Include(cp => cp.UserTabs)
                .Where(x => x.QrToken == qrToken && x.IsActive == true);
        }

        public static IQueryable<ContextPart> QrTokenExist(this IQueryable<ContextPart> query, string qrToken)
        {
            return query.Where(cp => cp.QrToken == qrToken && cp.IsActive);
        }

        public static IQueryable<ContextPart> CheckTables(this IQueryable<ContextPart> query)
        {
            return query
                .Include(cp => cp.UserTabs
                .Where(ut => !ut.IsClosed))
                .Where(cp => cp.IsActive && cp.UserTabs
                .Any(ut => !ut.IsClosed));
        }

        //public static IQueryable<ContextPart> CheckTables(this IQueryable<ContextPart> query, Guid cpId, Guid userId)
        //{
        //    return query.Where(cp => cp.Id == cpId)
        //        .SelectMany(cp => cp.UserTabs)
        //        .Where(ut => ut.UserId == userId && !ut.IsClosed)
        //        .Select(ut => new
        //        {
        //            ut.ContextId,
        //            ut.ContextPartId,
        //            ut.ContextPart
        //        })
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
