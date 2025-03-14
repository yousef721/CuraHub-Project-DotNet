using CMS.Models.CuraHub.IdentitySection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class PatientAppointmentCard
    {
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int PatientId { get; set; }
        public int ScheduleId { get; set; }
        public PatientAppointment PatientAppointment { get; set; } = null!;
    }
}
