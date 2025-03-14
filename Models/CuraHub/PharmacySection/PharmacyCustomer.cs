using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.PersonalDetails.CustomerSection;

namespace CMS.Models.CuraHub.PharmacySection
{
    public class PharmacyCustomer : Customer
    {
        public string? ApplicationUserId { get; set; } 
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? ApplicationUser{ get; set; }
        public List<PharmacyOrder>? PharmacyOrders { get; set; } 
    }
}
