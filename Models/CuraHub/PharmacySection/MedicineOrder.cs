using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PharmacySection
{
    public class MedicineOrder
    {
        public int MedicineId { get; set; }
        public Medicine Medicine { get; set; } = null!;

        public int MedicineCount { get; set; }

        public int PharmacyOrderId { get; set; }
        public PharmacyOrder PharmacyOrder { get; set; } = null!;
        
        
    }
}
