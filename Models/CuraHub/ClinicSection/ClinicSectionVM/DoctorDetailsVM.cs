using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class DoctorDetailsVM
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

        public int ExperienceYears { get; set; }
        public string MedicalDegree { get; set; } = null!;
        public double ConsultationDuration { get; set; }
        public double ConsultationFees { get; set; }
        public double Rate { get; set; }
        public string Title { get; set; } = null!;

        public string MedicalLicense { get; set; } = null!;
        public string MedicalRegistration { get; set; } = null!;
        public string MedicalIdentificationCard { get; set; } = null!;

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = null!;

        public List<Schedule> Schedules { get; set; } = null!;
        public List<ClinicReceptionist> ClinicReceptionists { get; set; } = null!;
        public List<RequestClinicReceptionist> RequestClinicReceptionists { get; set; } = new List<RequestClinicReceptionist>();

        public List<Qualification>? qualifications { get; set; }

        public List<QuestionAndAnswer>? QuestionAndAnswers { get; set; }
    }
}
