using AutoMapper;
using CMS.Data.Access.Layer.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Perestation.Layer.Areas.Customer.Controllers.CuraHub.Clinic
{
    public class ScheduleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public void ScheduleEdit(int ScheduleId)
        {
            var schedule = this._unitOfWork.ScheduleRepository.RetriveItem(e =>e.Id == ScheduleId);
            
        }
    }
}
