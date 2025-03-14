using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class RegisterVM
    {
        [MinLength(3 , ErrorMessage ="Min Length is 3")]
        [MaxLength(50, ErrorMessage = "Min Length is 50")]
        [Required (ErrorMessage = "First Name is Required")]
        //[RegularExpression("/^[a-zA-Z]{3,50}$/", ErrorMessage = "First Name must be only alphabet letters")]
        public string FirstName { get; set; } = null!;
        [MinLength(3, ErrorMessage = "Min Length is 3")]
        [MaxLength(50, ErrorMessage = "Min Length is 50")]
        [Required(ErrorMessage = "Last Name is Required")]
        //[RegularExpression("/^[a-zA-Z]{3,50}$/", ErrorMessage = "Last Name must be only alphabet letters")]
        public string LastName { get; set; } = null!;
        
        [DataType(DataType.EmailAddress , ErrorMessage = "This is not email")]
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]

        public string Password { get; set; } = null!;
        [Required(ErrorMessage = " confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "'Password' and 'ConfirmPassword' do not match.\r\n")]
        public string ConfirmPassword { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

    }
}
