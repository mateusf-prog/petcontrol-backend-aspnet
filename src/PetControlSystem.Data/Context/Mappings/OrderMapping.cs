using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Context.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .Property(o => o.Date)
                .IsRequired();

            builder
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder
                .HasMany(o => o.Products)
                .WithMany(p => p.Orders);

            builder
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
