using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class PatientAppointment
    {

        public DateOnly date { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;
        public int ScheduleId { get; set; }

        public Schedule Schedule { get; set; } = null!;

        public bool paid { get; set; } = false;

        public List<MedicalPrescription>? MedicalPrescriptions { get; set; } = null; 

    }
}
