using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.ReceptionistSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class ClinicReceptionist : Receptionist
    {

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
    }
}
