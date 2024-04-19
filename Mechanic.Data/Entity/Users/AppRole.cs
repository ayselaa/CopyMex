using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Entity.Users
{
    public class AppRole : IdentityRole
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
