using System;
using CMS.Models.Enums;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class PharmacyOrderVM
{
        public int Id { get; set; }
        public int Quentity { get; set; }
        public DateTime OrderDate { get; set; }
        public double Discount { get; set; }
        public double TotalPrice { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public int PharmacyCustomerId { get; set; }
        public PharmacyCustomerVM PharmacyCustomer { get; set; } = null!;
        public int PharmacyDeliveryRepresentativeId { get; set; }
        public PharmacyDeliveryRepresentativeVM? PharmacyDeliveryRepresentative { get; set; }
        public List<MedicineOrderVM>? MedicineOrderVM { get; set; } = new List<MedicineOrderVM>();
}
