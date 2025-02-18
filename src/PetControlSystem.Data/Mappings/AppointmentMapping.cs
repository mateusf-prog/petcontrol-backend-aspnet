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
                .HasOne(a => a.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CustomerId);

            builder
                .HasMany(a => a.PetSupports)
                .WithMany(ps => ps.Appointments);
        }
    }
}
