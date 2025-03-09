﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetControlSystem.Domain.Entities;

namespace PetControlSystem.Data.Mappings
{
    public class AppointmentPetSupportMapping : IEntityTypeConfiguration<AppointmentPetSupport>
    {
        public void Configure(EntityTypeBuilder<AppointmentPetSupport> builder)
        {
            builder
                .HasKey(aps => new { aps.AppointmentId, aps.PetSupportId }); 

            builder
                .HasOne(aps => aps.Appointment)
                .WithMany(a => a.AppointmentPetSupports)
                .HasForeignKey(aps => aps.AppointmentId);

            builder
                .HasOne(aps => aps.PetSupport)
                .WithMany(ps => ps.AppointmentPetSupports)
                .HasForeignKey(aps => aps.PetSupportId);
        }
    }
}
