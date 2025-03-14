using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisLabAppointment
    {

        public int Id { get; set; }
        public DateOnly Date { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeOnly Appointment { get; set; }
        public bool Available { get; set; }
        public int MedicalAnalysisLabBranchId { get; set; }
        public MedicalAnalysisLabBranch MedicalAnalysisLabBranch { get; set; } = null!;

        public int MedicalAnalysisLabCustomerId { get; set; }
        public MedicalAnalysisLabCustomer? MedicalAnalysisLabCustomer { get; set; }
    }
}
