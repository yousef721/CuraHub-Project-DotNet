using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Models.CuraHub.IdentitySection.IdentitySectionVM
{
    public class ApplicationRolesVM
    {
        public List<IdentityRole> roles { get; set; } = new List<IdentityRole>();

        public int TotalRoleCount { get; set; }

        public int CurrentPageIndex { get; set; }
    }
}
