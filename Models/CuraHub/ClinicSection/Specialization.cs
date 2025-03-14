using CMS.Models.CuraHub.QuestionAndAnswerSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Icon { get; set; }

        public List<Doctor>? Doctors { get; set; }

        public List<RequestDoctor>? RequestDoctors { get; set; }

        public List<QuestionAndAnswer>? QuestionAndAnswers { get; set; }

    }
}
