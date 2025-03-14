using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.QuestionAndAnswerConfigration
{
    public class QuestionAndAnswerConfig : IEntityTypeConfiguration<QuestionAndAnswer>
    {
        public void Configure(EntityTypeBuilder<QuestionAndAnswer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Question)
               .HasMaxLength(200)
               .IsUnicode(true);
            builder.Property(e => e.Answer)
                .HasMaxLength(1000)
                .IsUnicode(true);
            builder.Property(e => e.ApplicationUserId)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasOne(e => e.Doctor)
                .WithMany(e => e.QuestionAndAnswers)
                .HasForeignKey(e => e.DoctorId)
                ;

            builder.HasOne(e => e.Specialization)
               .WithMany(e => e.QuestionAndAnswers)
               .HasForeignKey(e => e.SpecializationId)
               .OnDelete(DeleteBehavior.NoAction);
            ;





            builder.ToTable("QuestionAndAnswers", "QuestionAndAnswer");

        }
    }
}
