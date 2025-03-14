using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class SchedulesVM
    {
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();

        public int TotalSchedulesCount { get; set; }
        public int currentPageNumber { get; set; }
    }
}
