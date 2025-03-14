using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.MedicalSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class RequestDoctor : Medical
    {
        public double ConsultationDuration { get; set; }
        public double ConsultationFees { get; set; }
        public double Rate { get; set; }

        public string MedicalLicense { get; set; } = null!;
        public string MedicalRegistration { get; set; } = null!;
        public string MedicalIdentificationCard { get; set; } = null!;

        public string Details { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = null!;

    }
}
