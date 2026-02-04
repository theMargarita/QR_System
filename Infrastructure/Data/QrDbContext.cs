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

            modelBuilder.HasPostgresExtension("uuid-ossp");

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()!.ToLower());
            }

            base.OnModelCreating(modelBuilder);

            //SeedData(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);


        }

        //guid id
        //private void SeedData(ModelBuilder modelBuilder)
        //{
        //    //CONTEXTPARTS
        //    //modelBuilder.Entity<ContextPart>().HasData(
        //    //    new ContextPart { Id = 1, Name = "Bord 1" },
        //    //    new ContextPart { Id = 2, Name = "Bord 2" },
        //    //    new ContextPart { Id = 3, Name = "Bord 3" },
        //    //    new ContextPart { Id = 4, Name = "Bord 4" },
        //    //    new ContextPart { Id = 5, Name = "Bord 5" },
        //    //    new ContextPart { Id = 6, Name = "Bord 6" }
        //    //    );

        //    //CONTEXTS
        //    modelBuilder.Entity<Context>().HasData(
        //        new Context { Id = 1, Name = "Ölkyl", OwnerId = 1, ContextPartIsUnique = true },
        //        new Context { Id = 2, Name = "Bar", OwnerId = 1, ContextPartIsUnique = true },
        //        new Context { Id = 3, Name = "Restaurang", OwnerId = 1, ContextPartIsUnique = true }
        //        );

        //    modelBuilder.Entity<Owner>().HasData(
        //        new Owner
        //        {
        //            Id = 1,
        //            Name = "Digital Creation", //admin 
        //        });


        //    //PRODUCTS
        //    modelBuilder.Entity<Product>().HasData(
        //        new Product { Id = 1, Name = "Coca Cola", Price = 25.0m },
        //        new Product { Id = 2, Name = "Öl", Price = 20.0m },
        //        new Product { Id = 3, Name = "Röd vin", Price = 22.0m },
        //        new Product { Id = 4, Name = "Sprite", Price = 18.0m },
        //        new Product { Id = 5, Name = "Water", Price = 10.0m },
        //        new Product { Id = 6, Name = "Pizza", Price = 30.0m },
        //        new Product { Id = 7, Name = "Pasta", Price = 28.0m },
        //        new Product { Id = 8, Name = "Salad", Price = 15.0m }
        //        );
        //}
    }
}