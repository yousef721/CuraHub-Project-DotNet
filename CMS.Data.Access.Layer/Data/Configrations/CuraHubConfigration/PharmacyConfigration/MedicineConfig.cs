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
    public class MedicineConfig : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
                
            builder.Property(e => e.Description)
                 .IsRequired()
                 .HasMaxLength(1000)
                 .IsUnicode(true);

            builder.Property(e => e.Status)
                 .IsRequired(false)
                 .HasMaxLength(50)
                 .IsUnicode(true);


            builder.Property(e => e.Img)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.HasOne(e=>e.PharmacyCategory)
                .WithMany(e=>e.Medicines)
                .HasForeignKey(e=>e.PharmacyCategoryId)
                ;

            builder.HasMany(e => e.MedicineOrders)
                .WithOne(e => e.Medicine)
                .HasForeignKey(e => e.MedicineId)
                ;
            builder.HasOne(e => e.MedicineManufactory)
                .WithMany(e => e.Medicines)
                .HasForeignKey(e => e.MedicineManufactoryId)
                ;

            builder.ToTable("Medicines", "Pharmacy");


        }
    }
}
