using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models.CuraHub.MedicalAnalysisLabSection;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.MedicalAnalysisLabConfigration
{
    public class MedicalAnalysisLabCustomerConfig : IEntityTypeConfiguration<MedicalAnalysisLabCustomer>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisLabCustomer> builder)
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


            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(true);
            builder.Property(e => e.BloodType)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(true);
            builder.Property(e => e.PersonalNationalIDCard)
                .HasMaxLength(100)
                .IsUnicode(true);
            
            builder.Property(e => e.PersonalNationalIDNumber)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);


            builder.HasMany(e => e.MedicalAnalysisTestResults)
                .WithOne(e => e.MedicalAnalysisLabCustomer)
                .HasForeignKey(e => e.MedicalAnalysisLabCustomerId);

            builder.HasMany(e => e.MedicalAnalysisLabAppointments)
                .WithOne(e => e.MedicalAnalysisLabCustomer)
                .HasForeignKey(e => e.MedicalAnalysisLabCustomerId);
            
            builder.HasMany(e => e.MedicalAnalysisTestCustomers)
                .WithOne(e => e.MedicalAnalysisLabCustomer)
                .HasForeignKey(e => e.MedicalAnalysisLabCustomerId);




            builder.ToTable("MedicalAnalysisLabCustomers", "MedicalAnalysisLab");

        }
    }
}
