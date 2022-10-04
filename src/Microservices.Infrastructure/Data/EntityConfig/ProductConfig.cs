using Microservices.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.Infrastructure.Data.EntityConfig
{
    public class ProductConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> b)
        {
            b.ToTable("Product");

            b.HasKey(e => e.Id);
        }
    }
}