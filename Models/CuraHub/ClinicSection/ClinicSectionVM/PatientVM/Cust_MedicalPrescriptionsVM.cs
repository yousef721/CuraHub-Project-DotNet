using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM
{
    public class Cust_MedicalPrescriptionsVM
    {
        public Schedule Schedule { get; set; } = new Schedule();
        public Patient Patient { get; set; } = new Patient();
       
        public DateOnly date {  get; set; }
        public List<MedicalPrescription> MedicalPrescriptions { get; set; } = new List<MedicalPrescription>();
    }
}
