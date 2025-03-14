using CMS.Models.CuraHub.ClinicSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM
{
    public class Admin_QuestionsVM
    {
        public List<QuestionAndAnswer> QuestionAndAnswers = new List<QuestionAndAnswer>();
        public int PageNumber {  get; set; }
        public int TotalQuestionCount { get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
        public List<Specialization> Specializations { get; set; } = new List<Specialization>();
    }
}
