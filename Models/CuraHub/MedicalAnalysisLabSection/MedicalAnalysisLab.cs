using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisLab
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public List<MedicalAnalysisLabBranch> MedicalAnalysisLabBranches { get; set; } = new List<MedicalAnalysisLabBranch>();

        public List<MedicalAnalysisTest> MedicalAnalysisTests { get; set; } = new List<MedicalAnalysisTest>();
    }
}
