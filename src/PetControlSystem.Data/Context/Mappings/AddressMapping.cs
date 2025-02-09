using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Context.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder
                .Property(a => a.Street)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(a => a.Number)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder
                .Property(a => a.Complement)
                .HasColumnType("varchar(100)");

            builder
                .Property(a => a.Neighborhood)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(a => a.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(a => a.State)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(a => a.PostalCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder
                .HasOne(a => a.Customer)
                .WithOne(c => c.Address)
                .HasForeignKey<Address>(a => a.CustomerId);

            builder
                .HasOne(a => a.User)
                .WithOne(u => u.Address)
                .HasForeignKey<Address>(a => a.UserId);
        }
    }
}
