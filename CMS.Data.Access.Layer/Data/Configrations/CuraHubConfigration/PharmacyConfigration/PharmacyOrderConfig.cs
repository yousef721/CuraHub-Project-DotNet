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
    public class PharmacyOrderConfig : IEntityTypeConfiguration<PharmacyOrder>
    {
        public void Configure(EntityTypeBuilder<PharmacyOrder> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.PharmacyDeliveryRepresentative)
                 .WithMany(e => e.PharmacyOrders)
                 .HasForeignKey(e => e.PharmacyDeliveryRepresentativeId)
                 .IsRequired(false);

            builder.HasOne(e => e.PharmacyCustomer)
                .WithMany(e => e.PharmacyOrders)
                .HasForeignKey(e => e.PharmacyCustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
                

            builder.HasMany(e => e.MedicineOrders)
                .WithOne(e => e.PharmacyOrder)
                .HasForeignKey(e => e.PharmacyOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("PharmacyOrders", "Pharmacy");

        }
    }
}
