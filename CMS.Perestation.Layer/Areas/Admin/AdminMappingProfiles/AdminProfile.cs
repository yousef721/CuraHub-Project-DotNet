using AutoMapper;
using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestDoctorSectionVM;
using CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;
using CMS.Models.CuraHub.PharmacySection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.RequestClinicReceptionistSectionVM;

namespace CMS.Perestation.Layer.Areas.Admin.AdminMappingProfiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserVM>().ReverseMap();
            CreateMap<ApplicationUser, ProfileVM>().ReverseMap();
            CreateMap<Doctor, DoctorVM>().ReverseMap();
            CreateMap<Doctor, DoctorCreateEditVM>().ReverseMap();
            CreateMap<Doctor, DoctorDetailsVM>().ReverseMap();

            CreateMap<ClinicReceptionist, ClinicReceptionistCreateEditVM>().ReverseMap();

            CreateMap<ClinicReceptionist, ClinicReceptionistDetailsVM>().ReverseMap();

            CreateMap<Doctor, RequestDoctor>().ReverseMap();


            CreateMap<RequestDoctorDetailsVM,RequestDoctor>().ReverseMap();

            CreateMap<RequestClinicReceptionistDetailsVM, RequestClinicReceptionist>().ReverseMap();

            CreateMap<RequestClinicReceptionist, ClinicReceptionist>().ReverseMap();

             

            CreateMap<Qualification, QualificationCreateEditVM>().ReverseMap();

            CreateMap<Specialization, SpecializationVM>().ReverseMap();


            // Pharmacy

            CreateMap<Medicine, MedicineVM>().ReverseMap();
            CreateMap<MedicineManufactory, MedicineManufactoryVM>().ReverseMap();

            CreateMap<PharmacyCategory, PharmacyCategoryVM>().ReverseMap();
            CreateMap<Pharmacist, PharmacistVM>().ReverseMap();
            CreateMap<PharmacyOrder, PharmacyOrderVM>().ReverseMap();
            CreateMap<PharmacyCustomer, PharmacyCustomerVM>().ReverseMap();
            CreateMap<PharmacyDeliveryRepresentative, PharmacyDeliveryRepresentativeVM>().ReverseMap();
            CreateMap<MedicineOrder, MedicineOrderVM>().ReverseMap();

        }
    }
}
