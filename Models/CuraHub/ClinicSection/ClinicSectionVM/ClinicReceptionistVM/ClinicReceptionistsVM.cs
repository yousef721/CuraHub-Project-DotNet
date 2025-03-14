using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM
{
    public class ClinicReceptionistsVM
    {
        public List<ClinicReceptionist> clinicReceptionists { get; set; } = new List<ClinicReceptionist>();
        public List<Doctor> doctors { get; set; } = new List<Doctor>();

        public int TotalClinicReceptionistCount { get; set; }
        public int CurrentPage { get; set; }

    }
}
