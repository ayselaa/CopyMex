using Mechanic.Data.Entity.Enums;
using Mechanic.Data.Entity.Users;
using static Mechanic.Web.Areas.Admin.Controllers.UserController;

namespace Mechanic.Web.Areas.Admin.ViewModels.User
{
    public class UserVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        //public Role SelectedRole { get; set; }
        public UserRoles SelectedRole { get; set; }

    }


}
