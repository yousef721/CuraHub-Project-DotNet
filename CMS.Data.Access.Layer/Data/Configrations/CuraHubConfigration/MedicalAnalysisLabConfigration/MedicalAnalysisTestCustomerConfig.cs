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
    public class MedicalAnalysisTestCustomerConfig : IEntityTypeConfiguration<MedicalAnalysisTestCustomer>
    {
        public void Configure(EntityTypeBuilder<MedicalAnalysisTestCustomer> builder)
        {
            builder.HasKey(e =>new {e.MedicalAnalysisLabCustomerId ,e.MedicalAnalysisTestId});

            builder.HasOne(e => e.MedicalAnalysisLabCustomer)
                .WithMany(e => e.MedicalAnalysisTestCustomers)
                .HasForeignKey(e => e.MedicalAnalysisLabCustomerId);

            builder.HasOne(e => e.MedicalAnalysisTest)
                .WithMany(e => e.MedicalAnalsisTestCustomers)
                .HasForeignKey(e => e.MedicalAnalysisTestId);

            builder.ToTable("MedicalAnalysisTestCustomers", "MedicalAnalysisLab");

        }
    }
}
