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

            builder.HasOne(u => u.Address)
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.AddressId);
        }
    }
}
