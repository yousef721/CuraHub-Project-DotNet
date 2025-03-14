using System;
using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IPharmacyRepository;
using CMS.Models.CuraHub.PharmacySection;

namespace CMS.Data.Access.Layer.Repository.CuraHubRepository.PharmacyRepository;

public class PharmacyCartRepository : Repository<PharmacyCart>, IPharmacyCartRepository
{
    public PharmacyCartRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
}
