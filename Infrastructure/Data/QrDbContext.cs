using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class QrDbContext : DbContext
    {
        public QrDbContext(DbContextOptions<QrDbContext> options) : base(options)
        {
        }

        //public DbSet<Role> Roles { get; set; }
        public DbSet<Context> Contexts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserTab> UserTabs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
