using Domain.Models;
using Domian.Models;
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
        public DbSet<ContextPart> ContextParts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserTab> UserTabs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //to "default" to restrict delete behavior
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var fk in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => 
                e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
