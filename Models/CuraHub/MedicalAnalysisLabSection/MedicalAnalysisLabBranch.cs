using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisLabBranch
    {
        public int Id { get; set; }
        public string State { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Street { get; set; } = null!;

        public TimeOnly StartWork { get; set; }
        public TimeOnly EndWork { get; set; }
        public TimeOnly AnalysisDuration { get; set; }
        public int  MedicalAnalysisLabId { get; set; }
        public MedicalAnalysisLab MedicalAnalysisLab { get; set; } = null!;

        public List<MedicalAnalysisSpecialist>? MedicalAnalysisSpecialists { get; set; } 
       
        public List<RequestMedicalAnalysisSpecialist>? RequestMedicalAnalysisSpecialists { get; set; }

        public List<MedicalAnalysisLabReceptionist>? MedicalAnalysisReceptionists { get; set; }
                    
        public List<RequestMedicalAnalysisLabReceptionist>? RequestMedicalAnalysisReceptionists { get; set; } 


        public List<MedicalAnalysisLabAppointment>? MedicalAnalysisLabAppointments { get; set; }

    }
}
