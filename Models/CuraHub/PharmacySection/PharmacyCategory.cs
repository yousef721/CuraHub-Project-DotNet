using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.PharmacySection
{
    public class PharmacyCategory
    {
        public int Id { get; set; }
        public string? Img { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public List<Medicine>? Medicines { get; set; } 
    }
}
