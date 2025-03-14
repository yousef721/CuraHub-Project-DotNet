using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.MedicalAnalysisLabSection
{
    public class MedicalAnalysisTest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public double Discount { get; set; }
        public double Price { get; set; }
        public TimeOnly Duration { get; set; }

        public int MedicalAnalysisLabId { get; set; }
        public MedicalAnalysisLab? MedicalAnalysisLab { get; set; }

        public List<MedicalAnalysisTestCustomer>? MedicalAnalsisTestCustomers { get; set; }
        public List<MedicalAnalysisTestResult>? MedicalAnalysisTestResults { get; set; }
        

    }
}
