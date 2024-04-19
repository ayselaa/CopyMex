using System.ComponentModel.DataAnnotations;

namespace Mechanic.Web.Areas.Admin.ViewModels.Account
{
    public class LoginModelVM
    {
        [Required(ErrorMessage = "Login yazılmıyıb")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Parol yazılmıyıb")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Yadda Saxlamaq?")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
