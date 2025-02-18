using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class PetMapping : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder
                .Property(p => p.Weight)
                .IsRequired();

            builder
                .Property(p => p.Type)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.Gender)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(100)");

            builder
                .HasOne(p => p.Customer)
                .WithMany(c => c.Pets)
                .HasForeignKey(p => p.CustomerId);
        }
    }
}
