using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class ApplicationUserVM
    {
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        public bool LockoutEnabled { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public string ProfilePicture { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
