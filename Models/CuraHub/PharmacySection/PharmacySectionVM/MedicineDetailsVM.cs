using System;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class MedicineDetailsVM
{
    public Medicine Medicine { get; set; } = new Medicine();
    public PharmacyCart PharmacyCart { get; set; } = new PharmacyCart();
    public string? UserId { get; set; }
}
