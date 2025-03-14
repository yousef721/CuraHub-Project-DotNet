using CMS.Models.CuraHub.IdentitySection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM
{
    public class RequestClinicReceptionistDetailsVM
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string Gender { get; set; } = null!;

        public string Details { get; set; } = null!;

        public string? BloodType { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string ProfilePicture { get; set; } = null!;
        public string PersonalNationalIDNumber { get; set; } = null!;

        public string PersonalNationalIDCard { get; set; } = null!;


        public string MaritalStatus { get; set; } = null!;


        public TimeOnly StartWork { get; set; }
        public TimeOnly EndWork { get; set; }

        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public double Salary { get; set; }


        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
