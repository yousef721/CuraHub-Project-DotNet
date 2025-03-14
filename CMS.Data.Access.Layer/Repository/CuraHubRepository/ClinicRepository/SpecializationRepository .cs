using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IClinicRepository;
using CMS.Models.CuraHub.ClinicSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository.CuraHubRepository.ClinicRepository
{
    public class SpecializationRepository : Repository<Specialization>, ISpecializationRepository
    {
        public SpecializationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
