using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.History
{
    public class PatientHistoryCreateVM
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Description { get; set; } = null!;
        public int PatientId { get; set; }

        public Patient Patient { get; set; } = null!;

    }

}
