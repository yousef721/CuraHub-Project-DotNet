using CMS.Models.CuraHub.IdentitySection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PersonalDetails.EmployeeSection
{
    [NotMapped]
    public abstract class Employee : PersonalDetails
    {


        [RegularExpression("^(Male|Female)$", ErrorMessage = "Gender must be 'Male' or 'Female'.")]
        public string Gender { get; set; } = null!;

        [RegularExpression("^(A+|A-|B+|B-|AB+|AB-|O+|O-)$", ErrorMessage = "Blood Type must be 'A+','A-','B+','B-','AB+','AB-','O+','O-'")]
        public string? BloodType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ProfilePicture { get; set; } = null!;
        public string PersonalNationalIDNumber { get; set; } = null!;

        public string PersonalNationalIDCard { get; set; } = null!;


        [RegularExpression("^(Single|Married)$", ErrorMessage = "Marital Status must be 'Single' or 'Married'.")]
        public string MaritalStatus { get; set; } = null!;


        public TimeOnly StartWork { get; set; }
        public TimeOnly EndWork { get; set; }

        public string ApplicationUserId { get; set; } = null!;



    }
}
