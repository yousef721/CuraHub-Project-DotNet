using CMS.Models.CuraHub.PersonalDetails.EmployeeSection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PersonalDetails.EmployeeSection.MedicalSection
{
    [NotMapped]
    public abstract class Medical : Employee
    {
        public int ExperienceYears { get; set; }
        public string MedicalDegree { get; set; } = null!;
    }
}
