using CMS.Models.CuraHub.MedicalAnalysisLabSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.MedicalAnalysisLabConfigration
{
    internal class MedicalAnalysisTestResultConfig : IEntityTypeConfiguration<MedicalAnalysisTestResult>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisTestResult> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Result)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(true);
            builder.Property(e => e.Report)
                .IsRequired()
                .HasMaxLength(150)
                .IsUnicode(true);

            builder.HasOne(e => e.MedicalAnalysisLabCustomer)
                .WithMany(e => e.MedicalAnalysisTestResults)
                .HasForeignKey(e=>e.MedicalAnalysisLabCustomerId);

            builder.HasOne(e => e.MedicalAnalysisTest)
                .WithMany(e => e.MedicalAnalysisTestResults)
                .HasForeignKey(e => e.MediacalAnalysisTestId);

            builder.ToTable("MedicalAnalysisTestResults", "MedicalAnalysisLab");

        }
    }
}
