using System;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class MedicineOrderVM
{
    public int MedicineId { get; set; }
    public Medicine Medicine { get; set; } = null!;

    public int MedicineCount { get; set; }

    public int PharmacyOrderId { get; set; }
    public PharmacyOrder PharmacyOrder { get; set; } = null!;
    
}
