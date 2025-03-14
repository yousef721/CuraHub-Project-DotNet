using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class UserRoleVM
    {
        public ApplicationUser User { get; set; }
        public List<IdentityRole> Roles { get; set; }
    }
}
