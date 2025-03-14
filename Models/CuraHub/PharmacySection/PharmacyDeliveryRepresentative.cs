using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.DeliveryRepresentativeSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PharmacySection
{
    public class PharmacyDeliveryRepresentative : DeliveryRepresentative
    {
        public List<PharmacyOrder> PharmacyOrders { get; set; } = new List<PharmacyOrder>();

    }
}
