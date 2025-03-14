using CMS.Models.CuraHub.ClinicSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.ClinicConfigration
{
    public class PatientAppointmentConfig : IEntityTypeConfiguration<PatientAppointment>
    {
        public void Configure(EntityTypeBuilder<PatientAppointment> builder)
        {
            builder.HasKey(e => new { e.PatientId, e.ScheduleId });

            builder.HasOne(e => e.Patient)
                .WithMany(e => e.PatientAppointments)
                .HasForeignKey(e => e.PatientId);

            builder.HasOne(e => e.Schedule)
                .WithMany(e => e.PatientAppointments)
                .HasForeignKey(e => e.ScheduleId);

            builder.HasMany(e => e.MedicalPrescriptions)
                .WithOne(e => e.PatientAppointment)
                .HasForeignKey(e => new { e.PatientId, e.ScheduleId });

            builder.ToTable(name: "PatientAppointments", schema: "Clinic");

        }
    }
}
