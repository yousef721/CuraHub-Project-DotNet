using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class Cust_DoctorDetailsVM
    {
        public Doctor Doctor { get; set; } = new Doctor();


        public bool DetailsForUser { get; set; } = false;

        public List<Schedule> Schedules { get; set; } = new List<Schedule> ();

        public List<DoctorReview> DoctorReviews { get; set; } = new List<DoctorReview> ();  
    }
}
