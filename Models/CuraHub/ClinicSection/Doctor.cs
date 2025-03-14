using CMS.Models.CuraHub.IdentitySection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.MedicalSection;
using CMS.Models.CuraHub.QuestionAndAnswerSection;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class Doctor : Medical
    {

        public double ConsultationDuration { get; set; }
        public double ConsultationFees { get; set; }
        public double Rate { get; set; }
        public int userRatingCount { get; set; }

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

        public List<DoctorReview>? DoctorReviews { get; set; }


    }
}
