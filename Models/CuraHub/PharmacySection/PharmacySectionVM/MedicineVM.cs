using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class MedicineVM
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;
    public string Status { get; set;}  = null!;
    public string Description { get; set; } = null!;
    [Range(0, double.MaxValue, ErrorMessage = "Price cannot be negative")]
    [Required]
    public double Price { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative")]
    [Required]
    public int Quantity { get; set; }
    public string Img { get; set; }  = null!;
    [Required(ErrorMessage = "Please upload a file.")]
    [DataType(DataType.Upload)]
    public IFormFile File { get; set; }  = null!;
    
    [Required(ErrorMessage = "Please select a pharmacy category.")]
    public int PharmacyCategoryId { get; set; }
    public PharmacyCategoryVM? PharmacyCategory { get; set; }

    [Required(ErrorMessage = "Please select a manufactory.")]
    public int MedicineManufactoryId { get; set; }
    public MedicineManufactoryVM? MedicineManufactory { get; set; }
    
}
