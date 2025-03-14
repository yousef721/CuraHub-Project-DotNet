using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.PatientVM
{
    public class Admin_PatientsVM
    {
        public List<Patient>? Patients {  get; set; }
        public int TotalPatientCount {  get; set; }
        public int CurrentPageNumber {  get; set; }
        
    }
}
