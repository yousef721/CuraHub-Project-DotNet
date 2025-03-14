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
    public class PharmacyCategoryConfig : IEntityTypeConfiguration<PharmacyCategory>
    {
        public void Configure(EntityTypeBuilder<PharmacyCategory> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(true);
            builder.Property(e => e.Description)
                 .HasMaxLength(1000)
                 .IsUnicode(true);

            builder.HasMany(e => e.Medicines)
                .WithOne(e => e.PharmacyCategory)
                .HasForeignKey(e => e.PharmacyCategoryId);

            builder.HasMany(e => e.Medicines)
                .WithOne(e => e.PharmacyCategory)
                .HasForeignKey(e => e.PharmacyCategoryId);

            builder.ToTable("PharmacyCategories", "Pharmacy");
        }
    }
}
