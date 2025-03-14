using CMS.Models.CuraHub.QuestionAndAnswerSection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public class DoctorVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string ProfilePicture { get; set; } = null!;
        public string PersonalNationalIDNumber { get; set; } = null!;
        public string Title { get; set; } = null!;      
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; } = null!;

        public List<Schedule> Schedules { get; set; } = new List<Schedule>();

    }
}
