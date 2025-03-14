using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ForCustomerSection.DoctorVMSection
{
    public class Cust_DoctorsVM
    {
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<Specialization> Specializations { get; set; } = new List<Specialization>();

        public List<Schedule> Schedules { get; set; } = new List<Schedule> ();
        public int TotalDoctorCount {  get; set; }
        public int CurrentPage { get; set; }


        public string? State { get; set; } = null;
        public string? City { get; set; } = null;
        public int? SpecializationId { get; set; } = null;
        public string? query {  get; set; }= null;
        public string? Title { get; set; } = null;

        public int? Rate { get; set; } = null;
        public int? ConsultationFees { get; set; } = null;
        public string? Gender { get; set; } = null;

        public Cust_DoctorsVM()
        {
            Rate = 0;
            ConsultationFees = 1000;
        }
    }
}
