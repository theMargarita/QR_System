using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public interface IOrderDbContext
    {
        DbSet<Context> Contexts { get; set; }
        DbSet<ContextPart> ContextParts { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<UserTab> UserTabs { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<Owner> Owners { get; set; }

    }

    public class QrDbContext : DbContext, IOrderDbContext
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
        public DbSet<Owner> Owners { get; set; }

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

            modelBuilder.HasPostgresExtension("uuid-ossp");

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()!.ToLower());
            }

            modelBuilder.Entity<ContextPart>()
                .HasIndex(x => x.QrToken)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

            SeedData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


        }

        //guid id
        private void SeedData(ModelBuilder modelBuilder)
        {
            //PRODUCTS
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Coca Cola", Price = 25.0m },
                new Product { Id = 2, Name = "Öl", Price = 20.0m , Category = "Dryck", Description = "Öl som öl eller?", Status = ProductStatus.Active},
                new Product { Id = 3, Name = "Röd vin", Price = 22.0m,Category = "Dryck" , Description = "Le vin rouge pour un amore", Status = ProductStatus.Active },
                new Product { Id = 4, Name = "Sprite", Price = 18.0m, Category = "Dryck", Description = "Fizzy driink", Status = ProductStatus.Active},
                new Product { Id = 5, Name = "Water", Price = 10.0m, Category = "Dryck", Description = "Still water - tap water", Status = ProductStatus.Active},
                new Product { Id = 6, Name = "Pizza", Price = 30.0m, Category = "Mat", Description = "Nom nom italian pizza, tu veux une piece ou parfois deux pices? ", Status = ProductStatus.Active},
                new Product { Id = 7, Name = "Pasta", Price = 28.0m, Category = "Mat", Description = "La pasta - c'est viens en Italie et tres bon", Status = ProductStatus.Active},
                new Product { Id = 8, Name = "Salad", Price = 15.0m, Category = "Mat", Description = "La Salade - tres  mais aussie bon et simple no? Avec la fromage et les totmotes", Status = ProductStatus.Active}
                );
        }
    }
}