using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class Schedule
    {
        public int Id { get; set; }
        public DayOfWeek Day { get; set; }

        public TimeOnly Appointment { get; set; } 
        public bool Available { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public ICollection<PatientAppointment>? PatientAppointments { get; set; }

    }
}
