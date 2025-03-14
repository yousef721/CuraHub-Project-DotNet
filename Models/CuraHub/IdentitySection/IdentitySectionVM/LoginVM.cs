using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class LoginVM
    {
       
        [DataType(DataType.EmailAddress , ErrorMessage = "Invalid email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        public bool RemeberMe { get; set; }



    }
}
