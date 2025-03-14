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
    public class MedicineOrderConfig : IEntityTypeConfiguration<MedicineOrder>
    {
        public void Configure(EntityTypeBuilder<MedicineOrder> builder)
        {
            builder.HasKey(e => new {e.MedicineId, e.PharmacyOrderId });

            builder.HasOne(e => e.Medicine)
                .WithMany(e => e.MedicineOrders)
                .HasForeignKey(e => e.MedicineId);

            builder.HasOne(e => e.PharmacyOrder)
                .WithMany(e=>e.MedicineOrders)
                .HasForeignKey(e=>e.PharmacyOrderId);

            builder.ToTable("MedicineOrders", "Pharmacy");


        }
    }
}
