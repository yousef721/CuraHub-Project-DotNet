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
    public class DoctorReviewConfig : IEntityTypeConfiguration<DoctorReview>
    {
        public void Configure(EntityTypeBuilder<DoctorReview> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Review)
                .IsRequired()
                .HasMaxLength(1000)
                .IsUnicode(true);
           


            builder.HasOne(e => e.Doctor)
                .WithMany(e => e.DoctorReviews)
                .HasForeignKey(e => e.DoctorId);
            


            builder.ToTable(name: "DoctorReviews", schema: "Clinic");




        }
    }
}
