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

        public static IQueryable<ContextPart> ContextPart(this IQueryable<ContextPart> query)
        {
            return query.Include(cp => cp.Context)
                .Include(cp => cp.UserTabs
                .Where(ut => ut.Status == TabStatus.Open));
        }

        public static IQueryable<ContextPart> GetAllWithContextId(this IQueryable<ContextPart> query, Guid contextId)
        {
            return query
              .Where(cp => cp.Context.Id == contextId)
              .Include(cp => cp.Context)
              .Include(cp => cp.UserTabs.Where(ut => ut.Status == TabStatus.Open));
        }

        public static IQueryable<ContextPart> GetAll(this IQueryable<ContextPart> query)
        {
            return query.
                Include(cp => cp.Context)
              .Include(cp => cp.UserTabs
              .Where(ut => ut.Status == TabStatus.Open));
        }
    }
}
