using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.QuestionAndAnswerSection.QuestionsAndAnswersVM
{
    public class Cust_QuestionAndAnswerVM
    {
        public List<QuestionAndAnswer> Questions {  get; set; } = new List<QuestionAndAnswer>();

        public int CurrentPage { get; set; }
        public int TotalQuestionsCount { get; set; }

        public int DoctorId { get; set; }


    }
}
