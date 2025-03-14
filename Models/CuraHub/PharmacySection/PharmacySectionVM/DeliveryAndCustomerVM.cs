using System;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class DeliveryAndCustomerVM
{
    public List<PharmacyCustomer> PharmacyCustomer { get; set; } = new List<PharmacyCustomer>();
    public List<PharmacyDeliveryRepresentative> PharmacyDeliveryRepresentative { get; set; } = new List<PharmacyDeliveryRepresentative>();
    public PharmacyOrderVM PharmacyOrderVM { get; set; } = new PharmacyOrderVM();
}
