using CMS.Models.CuraHub.PersonalDetails.EmployeeSection;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.MedicalSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class RequestMedicalAnalysisSpecialist : Medical
    {
        public double ExpectedSalary { get; set; }

        public int MedicalAnalysisLabBranchId { get; set; }

        public MedicalAnalysisLabBranch MedicalAnalysisLabBranch { get; set; } = null!;

        public string Details { get; set; } = null!;
    }
}
