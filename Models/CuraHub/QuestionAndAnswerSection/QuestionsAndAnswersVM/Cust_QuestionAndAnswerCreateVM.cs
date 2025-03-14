using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM
{
    public class Cust_QuestionAndAnswerCreateVM
    {
        public List<Specialization> Specializations { get; set; }  = new List<Specialization>();

    }
}
