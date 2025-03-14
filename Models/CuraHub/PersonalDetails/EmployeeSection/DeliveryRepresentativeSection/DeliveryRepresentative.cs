using CMS.Models.CuraHub.PersonalDetails.EmployeeSection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PersonalDetails.EmployeeSection.DeliveryRepresentativeSection
{
    [NotMapped]
    public abstract class DeliveryRepresentative : Employee
    {
        public double Salary { get; set; }
    }
}
