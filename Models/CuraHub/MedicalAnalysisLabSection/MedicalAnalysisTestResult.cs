using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisTestResult
    {
        public int Id { get; set; } 
        public DateOnly Date { get; set; }
        public string Result { get; set; } = null!;
        public string Report { get; set; } = null!;
        public int MediacalAnalysisTestId { get; set; }

        public MedicalAnalysisTest MedicalAnalysisTest { get; set; } = null!;

        public int MedicalAnalysisLabCustomerId { get; set; }

        public MedicalAnalysisLabCustomer MedicalAnalysisLabCustomer { get; set; } = null!;

    }
}
