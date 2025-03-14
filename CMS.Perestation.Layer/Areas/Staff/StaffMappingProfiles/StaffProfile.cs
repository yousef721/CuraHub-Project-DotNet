using AutoMapper;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;

namespace CMS.Perestation.Layer.Areas.Staff.StaffMappingProfiles
{
    public class StaffProfile :  Profile
    {
        public StaffProfile()
        {
            CreateMap<ClinicReceptionist, ClinicReceptionistDetailsVM>().ReverseMap();
        }

        
    }
}
