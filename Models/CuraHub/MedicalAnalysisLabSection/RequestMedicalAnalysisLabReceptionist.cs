using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.ReceptionistSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class RequestMedicalAnalysisLabReceptionist : RequestReceptionist
    {

        public MedicalAnalysisLabBranch MedicalAnalysisLabBranch { get; set; } = null!;
        public int MedicalAnalysisLabBranchId { get; set; }
    }
}
