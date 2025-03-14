using CMS.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class QualificationCreateEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Certification { get; set; } = "certication.webp";
        public IFormFile CertificationFile { get; set; } 

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

        public List<Doctor> Doctors { get; set; }
        public CrudOption? CrudQualificationOption { get; set; } = CrudOption.Creating;

    }
}
