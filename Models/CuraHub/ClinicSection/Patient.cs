using CMS.Models.CuraHub.PersonalDetails.CustomerSection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class Patient : Customer
    {

        public DateOnly LastVisitDate { get; set; }
        public string PersonalNationalIDNumber { get; set; } = null!;
        public string PersonalNationalIDCard { get; set; } = null!;

        //[RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Blood Type must be 'A+','A-','B+','B-','AB+','AB-','O+','O-'")]
        public string? BloodType { get; set; }
        public string? MedicalAnalysis { get; set; }

        public string? ProfilePicture { get; set; }
        [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
        public string MaritalStatus { get; set; } = null!;
        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public string Gender { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public string Occupation { get; set; } = null!;

        public List<PatientHistory>? Histories { get; set; }
        public List<PatientAppointment>? PatientAppointments { get; set; }



        public string ApplicationUserId { get; set; } = null!;





    }
}
