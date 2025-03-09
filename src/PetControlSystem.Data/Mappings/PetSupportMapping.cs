using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class PetSupportMapping : IEntityTypeConfiguration<PetSupport>
    {
        public void Configure(EntityTypeBuilder<PetSupport> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder
                .Property(p => p.SmallDogPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder
                .Property(p => p.MediumDogPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder
                .Property(p => p.LargeDogPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");
        }
    }
}
