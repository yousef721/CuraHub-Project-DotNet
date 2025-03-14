using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace CMS.Models.CuraHub.PharmacySection.PharmacySectionVM;

public class PharmacyCategoryVM
{
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Img { get; set; } = null!;
        [Required(ErrorMessage = "Please upload a file.")]
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; } = null!;
        public string Description { get; set; }= null!;
}
