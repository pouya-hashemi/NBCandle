using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class LoginModel : BasePage
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty]
        public LoginVM LoginVM{ get; set; }

        public LoginModel(SignInManager<User> signInManager,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public void OnGet(string returnUrl)
        {

            ViewData["returnUrl"] = returnUrl;
        }
        public async Task<IActionResult> OnPost(string returnUrl=null)
        {

             returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(LoginVM.Username.StartsWith("0")?LoginVM.Username.Substring(1):LoginVM.Username);
                if (user==null)
                {
                    ModelState.AddModelError(string.Empty, "نام کاربری یافت نشد!!!");
                    return Page();
                }

                var result = await _signInManager.PasswordSignInAsync(LoginVM.Username,
                            LoginVM.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {

                    var roles= await _userManager.GetRolesAsync(user);
                    if (roles.Any(a=>a=="Vendor"))
                    {
                        return RedirectToPage("index", new { Area = "Vendor" });
                    }
                    return LocalRedirect(returnUrl);
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "به دلیل ورود ناموفق، حساب شما تا 1 دقیقه مسدود می باشد");
                    return Page();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "اطلاعات وارد شده صحیح نیست");
                    return Page();
                }
            }
            return Page();
        }
    }
}
