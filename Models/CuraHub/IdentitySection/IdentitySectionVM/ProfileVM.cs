using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class ProfileVM
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? UserName { get; set; }

        public string? ProfilePicture { get; set; }
    }
}
