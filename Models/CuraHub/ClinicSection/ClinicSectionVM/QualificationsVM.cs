using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class QualificationsVM
    {

        public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();   
        public int TotalQualificationsCount { get; set; }
        public int CurrentPageNumber { get; set; }

    }

}
