using Mechanic.Data.Entity.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Entity.Users
{
    public class AppUser: IdentityUser
    {
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        public string? DriverLincense { get; set; }

        [ForeignKey("UserData")]
        public string? UserDataFIN { get; set; } //nullable elave eledim 
        public UserData? UserData { get; set; }  //nullabla elave eledim

       
        public int? ContractId { get; set; }  //contracta gosterkme icin elave etdim 
        public virtual Contract Contract { get; set; } //contract icin sade user yukleyende yeniden bax

        public string? CategoryDriverLicence { get; set; }
        public DateTime? IssueDateDriverLicence { get; set; }
        public DateTime? ExpirationDateDriverLincence { get; set; }
        public List<AppUserRole> UserRoles { get; set; }
    }
}
