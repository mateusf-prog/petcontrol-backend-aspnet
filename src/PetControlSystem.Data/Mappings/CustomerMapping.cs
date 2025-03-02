using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.OwnsOne(c => c.Address, a =>
            {
                a.Property(ad => ad.Street)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                a.Property(ad => ad.Number)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                a.Property(ad => ad.Complement)
                    .HasColumnType("varchar(50)");

                a.Property(ad => ad.Neighborhood)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                a.Property(ad => ad.City)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                a.Property(ad => ad.State)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                a.Property(ad => ad.PostalCode)
                    .IsRequired()
                    .HasColumnType("varchar(8)");
            });

            builder.HasMany(c => c.Appointments)
                .WithOne(a => a.Customer)
                .HasForeignKey(a => a.CustomerId);
        }
    }
}
