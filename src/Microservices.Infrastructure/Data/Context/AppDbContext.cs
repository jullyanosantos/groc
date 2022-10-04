using Microservices.Domain.Entities;
using Microservices.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Infrastructure.Data.Context
{
    public class AppDbContext : DbContext
    {
        //public List<ProductEntity> Products;

        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            AddEntityConfig(builder);
        }
        public virtual DbSet<ProductEntity> Products { get; set; }

        private void AddEntityConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
        }

        //public class AppDbContext: DbContext
        //{
        //    public List<ProductEntity> Products;

        //    public AppDbContext()
        //    {
        //        DbSetProducts();
        //    }

        //    private void DbSetProducts()
        //    {
        //        Products = new List<ProductEntity>
        //        {
        //            new ProductEntity
        //            {
        //                Id = 1,
        //                Name = "Pepsi",
        //                Description = "Soft Drink",
        //                Price = 10
        //            },
        //            new ProductEntity
        //            {
        //                Id = 2,
        //                Name = "Fanta",
        //                Description = "Soft Drink",
        //                Price = 13
        //            },
        //            new ProductEntity
        //            {
        //                Id = 3,
        //                Name = "Pizza",
        //                Description = "Fast Food",
        //                Price = 25
        //            },
        //            new ProductEntity
        //            {
        //                Id = 4,
        //                Name = "French Fries",
        //                Description = "Fast Food",
        //                Price = 20
        //            }
        //        };
        //    }
        //}
    }
}