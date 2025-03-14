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
    public class MedicalAnalysisLabBranchConfig : IEntityTypeConfiguration<MedicalAnalysisLabBranch>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisLabBranch> builder)
        {
            builder.HasKey(e => e.Id);

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

            builder.HasOne(e => e.MedicalAnalysisLab)
                .WithMany(e => e.MedicalAnalysisLabBranches)
                .HasForeignKey(e => e.MedicalAnalysisLabId);





            builder.HasMany(e => e.MedicalAnalysisSpecialists)
                .WithOne(e => e.MedicalAnalysisLabBranch)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.HasMany(e => e.RequestMedicalAnalysisSpecialists)
                .WithOne(e => e.MedicalAnalysisLabBranch)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.HasMany(e => e.MedicalAnalysisReceptionists)
                .WithOne(e => e.MedicalAnalysisLabBranch)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.HasMany(e => e.RequestMedicalAnalysisReceptionists)
                .WithOne(e => e.MedicalAnalysisLabBranch)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.HasMany(e => e.MedicalAnalysisLabAppointments)
                .WithOne(e => e.MedicalAnalysisLabBranch)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.ToTable("MedicalAnalysisLabBranches", "MedicalAnalysisLab");





        }
    }
}
