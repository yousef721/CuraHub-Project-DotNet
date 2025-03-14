using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository.IQ_ARepository;
using CMS.Models.CuraHub.QuestionAndAnswerSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository.CuraHubRepository.Q_ARepository
{
    public class QuestionAndAnswerRepository : Repository<QuestionAndAnswer>, IQuestionAndAnswerRepository
    {
        public QuestionAndAnswerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
