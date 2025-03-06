using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder
                .Property(p => p.Stock)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasColumnType("varchar(400)");

            builder
                .HasMany(o => o.OrderProducts)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId);
        }
    }
}
