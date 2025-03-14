using CMS.Models.CuraHub.IdentitySection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class DoctorReview
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public DateOnly Date {  get; set; }
        public string Review {  get; set; } = null!;
    }
}
