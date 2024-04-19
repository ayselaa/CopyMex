using Mechanic.Web.Areas.Admin.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace Mechanic.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {


        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginModelVM { ReturnUrl = returnUrl });
        }


        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginModelVM model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(model.UserName);
        //        // if (!user.IsAcitve) ModelState.AddModelError("", "Istifadeci destiqlenmiyib  ");
        //        if (user == null)
        //        {
        //            ModelState.AddModelError("Password", "Login və ya şifrə düzgün deyil");
        //            return View(model);
        //        }


        //        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
        //        if (result.Succeeded)
        //        {
        //            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
        //            {

        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Login və ya şifrə düzgün deyil");
        //        }
        //    }
        //    return View(model);
        //}


        //public async Task<IActionResult> Logout()
        //{
        //    // удаляем аутентификационные куки
        //    await _signInManager.SignOutAsync();
        //    return Redirect("/home/index");
        //}
    }
}
