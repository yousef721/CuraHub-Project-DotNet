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
    internal class PharmacyCustomerConfig : IEntityTypeConfiguration<PharmacyCustomer>
    {
        public void Configure(EntityTypeBuilder<PharmacyCustomer> builder)
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


            builder.HasMany(e => e.PharmacyOrders)
                .WithOne(e => e.PharmacyCustomer)
                .HasForeignKey(e => e.PharmacyCustomerId);

            builder.ToTable("PharmacyCustomers", "Pharmacy");

        }
    }
}
