using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class MedicalPrescription
    {
        public int Id { get; set; }

        public string MedicineType { get; set; } = null!;
        public int numOfTaken {  get; set; }
        public string? Details { get; set; }

        public DateOnly Date {  get; set; }
        public int ScheduleId { get; set; }
        public int PatientId { get; set; }


        public PatientAppointment? PatientAppointment { get; set; }

    }
}
