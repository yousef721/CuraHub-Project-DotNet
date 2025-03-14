using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class TotalSpecializationsVM
    {
        public List<Specialization> specializations = new List<Specialization>();
        public int TotalSpecializationCount = 0;
        public int CurrentPage = 1;

    }
}
