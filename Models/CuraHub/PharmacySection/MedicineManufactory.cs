using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PharmacySection
{
    public class MedicineManufactory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Info { get; set; }
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Street { get; set; }

        public List<Medicine> Medicines { get; set; } = new List<Medicine>();

    }
}
