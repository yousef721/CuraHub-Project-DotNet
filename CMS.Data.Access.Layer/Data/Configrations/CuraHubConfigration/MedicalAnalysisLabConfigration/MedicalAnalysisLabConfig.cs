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
    public class MedicalAnalysisLabConfig : IEntityTypeConfiguration<MedicalAnalysisLab>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisLab> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(true);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.HasMany(e => e.MedicalAnalysisTests)
                .WithOne(e => e.MedicalAnalysisLab)
                .HasForeignKey(e => e.MedicalAnalysisLabId);

            builder.HasMany(e => e.MedicalAnalysisLabBranches)
            .WithOne(e => e.MedicalAnalysisLab)
            .HasForeignKey(e => e.MedicalAnalysisLabId);

            builder.ToTable("MedicalAnalysisLabs", "MedicalAnalysisLab");

        }
    }
}
