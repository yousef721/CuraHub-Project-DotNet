using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.History
{
    public class PatientHistoriesVM
    {
        public List<PatientHistory> Histories { get; set; } = new List<PatientHistory>();
        public int PatientId { get; set; }
    }
}
