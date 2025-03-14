using CMS.Models.CuraHub.IdentitySection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM
{
    public class PatientUpsartVM
    {

        public int Id { get; set; }

        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; } 
        public string? State { get; set; }
        public string? City { get; set; } 
        public string? Region { get; set; } 
        public string? Street { get; set; }


        public DateOnly LastVisitDate { get; set; }
        public string? PersonalNationalIDNumber { get; set; }
        public string? PersonalNationalIDCard { get; set; }

        //[RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Blood Type must be 'A+','A-','B+','B-','AB+','AB-','O+','O-'")]
        public string? BloodType { get; set; }
        public string? MedicalAnalysis { get; set; }
        public IFormFile? MedicalAnalysisFile { get; set; }

        public string? ProfilePicture { get; set; }
        [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
        public string? MaritalStatus { get; set; }
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public string? Gender { get; set; } 
        public DateOnly DateOfBirth { get; set; }
        public string? Occupation { get; set; }

        public string? ApplicationUserId { get; set; }
        public int DoctorId { get; set; }
        public PatientAppointment patientAppointment { set; get; }
        public PatientUpsartVM()
        {

            Id = 0;
            FirstName = null;
            LastName = null;
            Email = null;
            Phone = "+20+102-345-678";
            State = "Cairo";
            City = "Nasr City";
            Region = "Nasr City";
            Street = "Abbas El Akkad St";
            PersonalNationalIDNumber = null;
            PersonalNationalIDCard = "PersonalNationalIDCardDedault.jpg";
            BloodType = "B+";
            MedicalAnalysis = null;
            ProfilePicture = null;
            MaritalStatus = "Single";
            Gender = "Male";
            DateOfBirth = DateOnly.FromDateTime(DateTime.Now);

            Occupation = "Software Engineer";
            ApplicationUserId = null;
            DoctorId = 0;

            patientAppointment = new PatientAppointment();

        }
    }
}
