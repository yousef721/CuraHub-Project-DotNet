using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM
{
    public class Cust_RequestDoctorCreateVM
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string FirstName { get; set; } = null!;
        [Required]

        public string LastName { get; set; } = null!;
        [EmailAddress]
        [Required]

        public string Email { get; set; } = null!;
        [Required]

        public string Phone { get; set; } = null!;
        [Required]

        public string State { get; set; } = null!;
        [Required]

        public string City { get; set; } = null!;
        [Required]

        public string Region { get; set; } = null!;
        [Required]

        public string Street { get; set; } = null!;
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        [Required]

        public string Gender { get; set; } = null!;

        //[RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Blood Type must be 'A+','A-','B+','B-','AB+','AB-','O+','O-'")]
        [Required]

        public string? BloodType { get; set; }
        [Required]

        public DateOnly DateOfBirth { get; set; }
        [Required]

        public string ProfilePicture { get; set; } = null!;
        public IFormFile? ProfilePictureFile { get; set; }

        [Required]

        public string PersonalNationalIDNumber { get; set; } = null!;
        [Required]

        public string PersonalNationalIDCard { get; set; } = null!;


        [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
        [Required]

        public string MaritalStatus { get; set; } = null!;

        [Required]

        public TimeOnly StartWork { get; set; }
        [Required]

        public TimeOnly EndWork { get; set; }

        [Required]
        public string ApplicationUserId { get; set; } = null!;
        [Required]

        public int ExperienceYears { get; set; }
        [Required]

        public string MedicalDegree { get; set; } = null!;
        [Required]

        public double ConsultationDuration { get; set; }
        [Required]

        public double ConsultationFees { get; set; }
        [Required]

        public double Rate { get; set; }
        [Required]
        [RegularExpression("^(Professor|Lecturer|Consultant|Specialist)$", ErrorMessage = "Marital Status must be 'Professor' or 'Lecturer' or 'Consultant' or 'Specialist' .")]
        public string Title { get; set; } = null!;

        [Required]

        public string MedicalLicense { get; set; } = null!;
        [Required]

        public string MedicalRegistration { get; set; } = null!;
        [Required]

        public string MedicalIdentificationCard { get; set; } = null!;
        [Required]

        public string Details { get; set; } = null!;


        /// <summary>

        public IFormFile? PersonalNationalIDCardFile { get; set; }
        public IFormFile? MedicalDegreeFile { get; set; }
        public IFormFile? MedicalLicenseFile { get; set; }
        public IFormFile? MedicalRegistrationFile { get; set; }
        public IFormFile? MedicalIdentificationCardFile { get; set; }

        /// </summary>




        [Required]

        public int SpecializationId { get; set; }
        [Required]

        public Specialization Specialization { get; set; } = null!;



        public List<Specialization> Specializations { get; set; } = null!;

        public List<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public Cust_RequestDoctorCreateVM()
        {
            this.Id = 0;
            this.FirstName = string.Empty;
            this.LastName = string.Empty;
            this.Email = string.Empty;
            this.Phone = string.Empty;
            this.State = string.Empty;
            this.City = string.Empty;
            this.Region = string.Empty;
            this.Street = string.Empty;
            this.Gender = "Male";
            this.BloodType = "B+";
            this.DateOfBirth = DateOnly.MinValue;
            this.ProfilePicture = "Profile.png";
            this.PersonalNationalIDNumber = string.Empty;
            this.PersonalNationalIDCard = "PersonalNationalIDCardDedault.jpg";
            this.MaritalStatus = "Single";
            this.StartWork = TimeOnly.MinValue;
            this.EndWork = TimeOnly.MinValue;
            this.ApplicationUserId = string.Empty;
            this.ExperienceYears = 0;
            this.MedicalDegree = "MedicalDegree.jpg";
            this.ConsultationDuration = 0.0;
            this.ConsultationFees = 0.0;
            this.Rate = 0.0;
            this.Title = "Professor";
            this.MedicalLicense = "MedicalLicense.webp";
            this.MedicalRegistration = "MedicalRegistration.jpg";
            this.MedicalIdentificationCard = "MedicalIdentificationCard.jpg";
            this.SpecializationId = 1;
            this.Details = "";

            this.PersonalNationalIDCardFile = null;
            this.MedicalDegreeFile = null;
            this.MedicalLicenseFile = null;
            this.MedicalRegistrationFile = null;
            this.MedicalIdentificationCardFile = null;
            this.ProfilePictureFile = null;
        }


    }
}

