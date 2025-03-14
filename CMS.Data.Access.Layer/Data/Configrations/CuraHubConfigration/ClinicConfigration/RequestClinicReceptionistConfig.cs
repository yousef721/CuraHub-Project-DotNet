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
    public class RequestClinicReceptionistConfig : IEntityTypeConfiguration<RequestClinicReceptionist>
    {
        public void Configure(EntityTypeBuilder<RequestClinicReceptionist> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
               .IsRequired()
               .HasMaxLength(50)
               .IsUnicode(true);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);
            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(true);

            builder.Property(e => e.State)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Region)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Street)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);


            builder.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(true);
            builder.Property(e => e.BloodType)
                .IsRequired()
                .HasMaxLength(5)
                .IsUnicode(true);

            builder.Property(e => e.ProfilePicture)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.PersonalNationalIDCard)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.MaritalStatus)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.PersonalNationalIDNumber)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);




            builder.Property(e => e.Details)
               .IsRequired()
               .HasMaxLength(1000)
               .IsUnicode(true);





            builder.HasOne(e => e.Doctor)
               .WithMany(e => e.RequestClinicReceptionists)
               .HasForeignKey(e => e.DoctorId);

            builder.ToTable(name: "RequestClinicReceptionists", schema: "Clinic");




        }
    }
}
