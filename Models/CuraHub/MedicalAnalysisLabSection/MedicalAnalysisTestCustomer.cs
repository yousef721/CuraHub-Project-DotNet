using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisTestCustomer
    {
        public int MedicalAnalysisTestId { get; set; }
        public MedicalAnalysisTest MedicalAnalysisTest { get; set; } = null!;

        public int MedicalAnalysisLabCustomerId { get; set; }

        public MedicalAnalysisLabCustomer MedicalAnalysisLabCustomer { get; set; } = null!;


    }
}
