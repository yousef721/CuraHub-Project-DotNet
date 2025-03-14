using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.ClinicSection.ClinicSectionVM
{
    public enum SpecializationsOperation
    {
        Creating ,
        Editing
    }
    public class SpecializationVM
    {
        public int Id { get; set; }

        [MinLength(3)]
        [Required]
        public string Name { get; set; } = null!;

        public string? Icon { get; set; }

        public SpecializationsOperation? specializationsOperation { get; set; } = SpecializationsOperation.Creating;

    }
}
