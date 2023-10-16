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
    public class ResetPasswordModel : BasePage
    {
        private readonly UserManager<User> _userManager;

        [BindProperty]
        public ResetPasswordVM ResetPasswordVM{ get; set; }
        public ResetPasswordModel(UserManager<User> userManager ,
            ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            this._userManager = userManager;
        }
        public void OnGet(string token,string phoneNumber)
        {
            ResetPasswordVM = new ResetPasswordVM()
            {
                PhoneNumber = phoneNumber,
                Token = token
            };

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _userManager.FindByNameAsync(ResetPasswordVM.PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "برای این شماره تماس، حساب کاربری وجود ندارد");
                return Page();
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, ResetPasswordVM.Token, ResetPasswordVM.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return Page();
            }
            return RedirectToPage("ResetPasswordConfimation");
        }
    }
}
