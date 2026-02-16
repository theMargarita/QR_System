using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class UserTabExtensions
    {
        public static IQueryable<UserTab> GetAllUserTabs(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Where(ut => ut.UserId == userId)
                .OrderByDescending(ut => ut.CreatedAt);
        }
        public static IQueryable<UserTab> GetAllUserTabsWithDetails(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Include(ut => ut.ContextPart)
                    .ThenInclude(cp => cp.Context)
                .Include(ut => ut.Transactions)
                .Include(ut => ut.Payments)
                .Where(ut => ut.UserId == userId)
                .OrderByDescending(ut => ut.CreatedAt);
        }

        public static IQueryable<UserTab> GetAllOpenUserTabs(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Include(ut => ut.ContextPart)
                .Where(ut => ut.UserId == userId
                          && ut.Status == TabStatus.Open)
                .OrderByDescending(ut => ut.CreatedAt);
        }

        public static IQueryable<UserTab> GetAllPaidUserTabs(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Include(ut => ut.ContextPart)
                .Where(ut => ut.UserId == userId
                          && ut.Status == TabStatus.Paid)
                .OrderByDescending(ut => ut.CreatedAt);
        }

        public static IQueryable<UserTab> GetAllClosedUserTabs(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Include(ut => ut.ContextPart)
                .Where(ut => ut.UserId == userId
                          && ut.Status == TabStatus.Closed)
                .OrderByDescending(ut => ut.ClosedAt);
        }

        public static IQueryable<UserTab> GetUserTabsToday(this IQueryable<UserTab> query, Guid userId)
        {
            var today = DateTimeOffset.UtcNow.Date;
            var tomorrow = today.AddDays(1);

            return query
                .Include(ut => ut.ContextPart)
                .Where(ut => ut.UserId == userId
                          && ut.CreatedAt >= today
                          && ut.CreatedAt < tomorrow)
                .OrderByDescending(ut => ut.CreatedAt);
        }

        public static IQueryable<UserTab> ClosedTabs(this IQueryable<UserTab> query)
        {
            return query.Where(ut => ut.Status == TabStatus.Closed);
        }

        public static IQueryable<UserTab> ActiveTabForUser(this IQueryable<UserTab> query, Guid cpId, Guid userId)
        {
            return query
                .Where(ut => ut.ContextPartId == cpId
                          && ut.UserId == userId
                          && ut.Status == TabStatus.Open);
        }

        public static IQueryable<UserTab> ActiveTabsAtTable(this IQueryable<UserTab> query, Guid contextPartId)
        {
            return query
                .Include(ut => ut.User)
                .Where(ut => ut.ContextPartId == contextPartId
                          && ut.Status == TabStatus.Open);
        }

        public static IQueryable<UserTab> AllActiveTabsForUser(this IQueryable<UserTab> query, Guid userId)
        {
            return query
                .Include(ut => ut.ContextPart)
                .Where(ut => ut.UserId == userId && ut.Status == TabStatus.Open);
        }

        public static IQueryable<UserTab> FullDetail(this IQueryable<UserTab> query)
        {
            return query
                .Include(ut => ut.Transactions)
                .Include(ut => ut.Payments)
                .Include(ut => ut.ContextPart)
                .Include(ut => ut.User);
        }
    }
}