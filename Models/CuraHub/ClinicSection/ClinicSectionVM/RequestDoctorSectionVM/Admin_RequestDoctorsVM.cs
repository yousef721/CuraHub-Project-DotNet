using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM
{
    public class Admin_RequestDoctorsVM
    {
        public List<RequestDoctor> RequestDoctors { get; set; } = new List<RequestDoctor>();

        public List<Specialization> Specializations { get; set; } = new List<Specialization> ();
        public int TotalRequestDoctorCount { get; set; }
        public int CurrentPageNumber { get; set; }
    }
}
