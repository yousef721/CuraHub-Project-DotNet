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
    public class PatientHistoryConfig : IEntityTypeConfiguration<PatientHistory>
    {
        public void Configure(EntityTypeBuilder<PatientHistory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(true);

            builder.HasOne(e => e.Patient)
                .WithMany(e => e.Histories)
                .HasForeignKey(e => e.PatientId);

            builder.ToTable(name: "PatientHistories", schema: "Clinic");


        }
    }
}
