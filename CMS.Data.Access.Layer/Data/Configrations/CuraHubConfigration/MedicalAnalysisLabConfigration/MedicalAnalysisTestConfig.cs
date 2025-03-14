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
    public class MedicalAnalysisTestConfig : IEntityTypeConfiguration<MedicalAnalysisTest>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisTest> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);

            builder.Property(e => e.Description)
                 .IsRequired()
                 .HasMaxLength(1000)
                 .IsUnicode(true);

            builder.HasOne(e => e.MedicalAnalysisLab)
                .WithMany(e => e.MedicalAnalysisTests)
                .HasForeignKey(e => e.MedicalAnalysisLabId);

            builder.HasMany(e => e.MedicalAnalsisTestCustomers)
                .WithOne(e => e.MedicalAnalysisTest)
                .HasForeignKey(e => e.MedicalAnalysisTestId);

            builder.HasMany(e => e.MedicalAnalysisTestResults)
                .WithOne(e => e.MedicalAnalysisTest)
                .HasForeignKey(e => e.MediacalAnalysisTestId);



            builder.ToTable("MedicalAnalysisTests", "MedicalAnalysisLab");

        }
    }
}
