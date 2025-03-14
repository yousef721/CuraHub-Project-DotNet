using AutoMapper;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.IdentitySection.IdentitySectionVM;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CMS.Perestation.Layer.Areas.Identity.IdentityMappingProfiles
{
    public class IdentityProfile : Profile
    {

        public IdentityProfile()
        {
            CreateMap<ApplicationUser , RegisterVM>().ReverseMap();
        }
    }
}
