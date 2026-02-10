using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<User> WithOpenTabs(this IQueryable<User> query) => query.Where(u => u.Tabs.Any(t => t.Status == TabStatus.Open));

        public static IQueryable<User> PhoneNumer(this IQueryable<User> query, string phoneNumber) => query.Where(u => u.PhoneNumber == phoneNumber);

        public static IQueryable<User>Search(this IQueryable<User> query, string searchTerm)
            => query
            .Where(u => u.FirstName
            .Contains(searchTerm) || u.LastName
            .Contains(searchTerm) || u.PhoneNumber != null && u.PhoneNumber.Contains(searchTerm))
            .OrderBy(u => searchTerm) // Prioritera matchning av söktermen i förnamn, efternamn och telefonnummer
            .ThenBy(u => u.Id)
            .ThenBy(u => u.FirstName)
            .ThenBy(u => u.LastName)
            .ThenBy(u => u.PhoneNumber)
            .ThenBy(u => u.CreatedAt)
            .Take(10);
    }
}
