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
    public class MedicalPrescriptionConfig : IEntityTypeConfiguration<MedicalPrescription>
    {
        public void Configure(EntityTypeBuilder<MedicalPrescription> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.MedicineType)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Details)
                .HasMaxLength(1000)
                .IsUnicode(true);

            builder.HasOne(e => e.PatientAppointment)
                .WithMany(e => e.MedicalPrescriptions)
                .HasForeignKey(e => new {e.PatientId , e.ScheduleId});

            

            builder.ToTable(name: "MedicalPrescriptions", schema: "Clinic");




        }
    }
}
