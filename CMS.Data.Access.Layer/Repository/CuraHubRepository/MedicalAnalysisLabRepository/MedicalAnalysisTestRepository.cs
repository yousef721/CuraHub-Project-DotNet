using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IMedicalAnalysisLabRepository;
using CMS.Models.CuraHub.MedicalAnalysisLabSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository.CuraHubRepository.MedicalAnalysisLabRepository
{
    public class MedicalAnalysisTestRepository : Repository<MedicalAnalysisTest>, IMedicalAnalysisTestRepository
    {
        public MedicalAnalysisTestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
