using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM
{
    public class Admin_RequestClinicReceptionistsVM
    {
        public List<RequestClinicReceptionist> RequestClinicReceptionists { get; set; } = new List<RequestClinicReceptionist>();

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public int TotalRequestClinicReceptionistCount { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
