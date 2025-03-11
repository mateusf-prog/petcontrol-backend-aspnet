using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class AppointmentMapping : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder
                .Property(a => a.Date)
                .IsRequired();

            builder
                .Property(a => a.Description)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder
                .Property(a => a.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(a => a.AppointmentPetSupports)
                .WithOne(aps => aps.Appointment)
                .HasForeignKey(aps => aps.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(a => a.Pet)
                .WithMany()
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
