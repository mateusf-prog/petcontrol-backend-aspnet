using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.DocumentType)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(2)");

            builder.Property(u => u.Phone)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(u => u.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

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
        }
    }
}
