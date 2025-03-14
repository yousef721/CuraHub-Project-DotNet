using System;
using System.ComponentModel.DataAnnotations.Schema;
using CMS.Models.CuraHub.IdentitySection;
using Microsoft.EntityFrameworkCore;

namespace CMS.Models.CuraHub.PharmacySection;

[PrimaryKey("MedicineId", "ApplicationUserId")]
public class PharmacyCart
{
    public int MedicineId { get; set; }
    [ForeignKey("MedicineId")]
    public Medicine Medicine { get; set; }
    public int count { get; set; }
    public string ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
}
