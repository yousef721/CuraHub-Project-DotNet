using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class MedicineManufactoryVM
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
}
