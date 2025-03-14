using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM
{
    public class Cust_RequestClinicReceptionistCreateVM
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

       
        public double ExpectedSalary { get; set; }
        [Required]

        public string Details { get; set; } = null!;


        /// <summary>

        public IFormFile? PersonalNationalIDCardFile { get; set; }
      

        /// </summary>




        [Required]

        public int DoctorId { get; set; }
        [Required]

        public Doctor Doctor { get; set; } = null!;



        public List<Doctor> Doctors { get; set; } = null!;

        public List<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public Cust_RequestClinicReceptionistCreateVM()
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
            this.ExpectedSalary = 0.0;
            this.DoctorId = 1;
            this.Details = "";
            this.PersonalNationalIDCardFile = null;
            this.ProfilePictureFile = null;
        }

    }
}
