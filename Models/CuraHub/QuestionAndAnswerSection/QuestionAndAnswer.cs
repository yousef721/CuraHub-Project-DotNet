using CMS.Models.CuraHub.ClinicSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.QuestionAndAnswerSection
{
    public class QuestionAndAnswer
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public DateTime? Date { get; set; }
        public bool Status { get; set; }
        public string? ApplicationUserId { get; set; }
        public int SpecializationId { get; set; } 
        public Specialization? Specialization { get; set; }
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
    }
}
