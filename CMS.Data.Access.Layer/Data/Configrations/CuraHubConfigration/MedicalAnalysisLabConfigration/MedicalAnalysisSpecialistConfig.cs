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
    public class MedicalAnalysisSpecialistCustomerConfig : IEntityTypeConfiguration<MedicalAnalysisSpecialist>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisSpecialist> builder)
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

            builder.Property(e => e.ProfilePicture)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.PersonalNationalIDNumber)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.PersonalNationalIDCard)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.MedicalDegree)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasOne(e => e.MedicalAnalysisLabBranch)
                .WithMany(e => e.MedicalAnalysisSpecialists)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.ToTable("MedicalAnalysisSpecialists", "MedicalAnalysisLab");

        }
    }
}
