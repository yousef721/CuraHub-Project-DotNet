using CMS.Models.CuraHub.PharmacySection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.PharmacyConfigration
{
    public class MedicineManufactoryConfig : IEntityTypeConfiguration<MedicineManufactory>
    {
        public void Configure(EntityTypeBuilder<MedicineManufactory> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Info)
                .IsRequired(false)
                .HasMaxLength(1000)
                .IsUnicode(true);

            builder.Property(e => e.Phone)
                .IsRequired(false)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.Email)
                .IsRequired(false)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(true);
            builder.Property(e => e.Region)
               .HasMaxLength(100)
               .IsUnicode(true);
            builder.Property(e => e.Street)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasMany(e => e.Medicines)
                .WithOne(e => e.MedicineManufactory)
                .HasForeignKey(e => e.MedicineManufactoryId);

            builder.ToTable("MedicineManufactories", "Pharmacy");


        }
    }
}
