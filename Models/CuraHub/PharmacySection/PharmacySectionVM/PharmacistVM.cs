using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters;
using CMS.Models.CuraHub.ClinicSection;
using Microsoft.AspNetCore.Http;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class PharmacistVM
{
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string ProfilePicture { get; set; } = null!;
        [Required(ErrorMessage = "Please upload a Profile Picture .")]
        [DataType(DataType.Upload)]
        public IFormFile? FileProfile { get; set; }
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Street { get; set; } = null!;
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public string Gender { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string PersonalNationalIDNumber { get; set; } = null!;
        public string PersonalNationalIDCard { get; set; } = null!;
        [Required(ErrorMessage = "Please upload a National ID Card.")]
        [DataType(DataType.Upload)]
        public IFormFile? FileNationalIDCard { get; set; }
        [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
        public string MaritalStatus { get; set; } = null!;
        public TimeOnly StartWork { get; set; }
        public TimeOnly EndWork { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public int ExperienceYears { get; set; }
        public string MedicalDegree { get; set; } = null!;
        [Required(ErrorMessage = "Please upload a MedicalDegree.")]
        [DataType(DataType.Upload)]
        public IFormFile FileMedicalDegree { get; set; } = null!;
        public double Salary { get; set; }
}  