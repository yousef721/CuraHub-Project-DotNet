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
    public class SpecializationConfig : IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e=>e.Icon)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasMany(e => e.Doctors)
                .WithOne(e => e.Specialization)
                .HasForeignKey(e => e.SpecializationId);

            builder.HasMany(e => e.QuestionAndAnswers)
                .WithOne(e => e.Specialization)
                .HasForeignKey(e => e.SpecializationId);

            builder.ToTable( name:"Specializations",schema:"Clinic");

        }
    }
}
