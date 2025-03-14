using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class PharmacyDeliveryRepresentativeVM 
{
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    public string ProfilePicture { get; set; } = null!;
    [Required(ErrorMessage = "Please upload a file.")]
    [DataType(DataType.Upload)]
    public IFormFile? FileProfile { get; set; }
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    public string Phone { get; set; } = null!;
    public string State { get; set; } = null!;
    public string City { get; set; } = null!;
    public string Region { get; set; } = null!;
    public string Street { get; set; } = null!;
    [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
    public string? Gender { get; set; }
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public string? PersonalNationalIDNumber { get; set; }
    public string PersonalNationalIDCard { get; set; } = null!;
    [Required(ErrorMessage = "Please upload a file.")]
    [DataType(DataType.Upload)]
    public IFormFile? FileNationalIDCard { get; set; }
    [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
    public string MaritalStatus { get; set; } = null!;
    public TimeOnly StartWork { get; set; }
    public TimeOnly EndWork { get; set; }
    public string ApplicationUserId { get; set; } = null!;
    public double Salary { get; set; }
    public List<PharmacyOrderVM> PharmacyOrders { get; set; } = new List<PharmacyOrderVM>();
}
