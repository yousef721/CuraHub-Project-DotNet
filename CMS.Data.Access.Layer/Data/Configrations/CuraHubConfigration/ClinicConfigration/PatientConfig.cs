using CMS.Models;
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
    public class PatientConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(true);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);


            builder.Property(e => e.PersonalNationalIDNumber)
                .IsRequired()
               .HasMaxLength(20)
               .IsUnicode(true);
            builder.Property(e => e.PersonalNationalIDCard)
               .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(true);

            builder.Property(e => e.BloodType)
               .HasMaxLength(4)
               .IsUnicode(false);

            builder.Property(e => e.Gender)
                .IsRequired()
               .HasMaxLength(7)
               .IsUnicode(true);

            builder.Property(e => e.MaritalStatus)
                .IsRequired()
               .HasMaxLength(10)
               .IsUnicode(true);

            builder.Property(e => e.Occupation)
               .IsRequired()
              .HasMaxLength(70)
              .IsUnicode(true);

            builder.Property(e => e.MedicalAnalysis)
               .HasMaxLength(100)
               .IsUnicode(false);

            builder.HasMany(e => e.Histories)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId);

            builder.HasMany(e => e.PatientAppointments)
                .WithOne(e => e.Patient)
                .HasForeignKey(e => e.PatientId);

            builder.Property(e => e.ApplicationUserId)
                .IsRequired()
               .HasMaxLength(100)
               .IsUnicode(true);

           
            builder.ToTable(name: "Patients", schema: "Clinic");



        }
    }
}
