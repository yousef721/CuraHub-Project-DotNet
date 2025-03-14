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
    public class PersonalDetailsConfig : IEntityTypeConfiguration<Qualification>
    {
        public void Configure(EntityTypeBuilder<Qualification> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(e => e.Certification)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasOne(e => e.Doctor)
                .WithMany(e => e.qualifications)
                .HasForeignKey(e => e.DoctorId);

            builder.ToTable(name: "Qualifications", schema: "Clinic");




        }
    }
}
