using Domain.Models;

namespace Infrastructure.Extensions
{
    public static class ProductExtension
    {
        public static IQueryable<Product> RemoveDeleted(this IQueryable<Product> query)
        {
            return query.Where(p => p.Status == ProductStatus.Inactive);
        }

        //both are the same
        public static IQueryable<Product> ActiveProducts(this IQueryable<Product> query)
            => query.Where(p => p.Status == ProductStatus.Active);

        public static IQueryable<Product> SearchProduct(this IQueryable<Product> query, string searchTerm)
        {
            return query
                .Where(p => p.Name.Contains(searchTerm) ||
                            p.Description.Contains(searchTerm) ||
                            p.Category!.Contains(searchTerm))
                .OrderBy(p => p.Name);
        }
        public static IQueryable<Product> Menu(this IQueryable<Product> query)
        {
            return query.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category
            });
        }
    }
}
