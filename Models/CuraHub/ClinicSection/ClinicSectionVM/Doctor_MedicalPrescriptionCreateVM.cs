using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class Doctor_MedicalPrescriptionCreateVM
    {
        public int ScheduleId { get; set; } 
        public int PatientId { get; set; }

        public PatientAppointment PatientAppointment { get; set; } = new PatientAppointment();

        public DateOnly date { get; set; }

        public string MedicineType1 { get; set; } = null!;
        public int numOfTaken1 { get; set; }
        public string? Details1 { get; set; }

        public string MedicineType2 { get; set; } = null!;
        public int numOfTaken2 { get; set; }
        public string? Details2 { get; set; }


        public string MedicineType3 { get; set; } = null!;
        public int numOfTaken3 { get; set; }
        public string? Details3 { get; set; }


        public string MedicineType4 { get; set; } = null!;
        public int numOfTaken4 { get; set; }
        public string? Details4 { get; set; }

        public string MedicineType5 { get; set; } = null!;
        public int numOfTaken5 { get; set; }
        public string? Details5 { get; set; }

    }
}
