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
    public class MedicalAnalysisLabAppointmentConfig : IEntityTypeConfiguration<MedicalAnalysisLabAppointment>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisLabAppointment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.MedicalAnalysisLabBranch)
                .WithMany(e => e.MedicalAnalysisLabAppointments)
                .HasForeignKey(e => e.MedicalAnalysisLabBranchId);

            builder.ToTable("MedicalAnalysisLabAppointments", "MedicalAnalysisLab");
            
        }
    }
}
