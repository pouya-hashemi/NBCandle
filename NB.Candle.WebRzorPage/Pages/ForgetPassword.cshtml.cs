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
using NB.Candle.WebRzorPage.Services.Email;
using NB.Candle.WebRzorPage.Services.Sms;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class ForgetPasswordModel : BasePage
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;

        [BindProperty]
        public ForgetPasswordVM ForgetPasswordVM { get; set; }
        public ForgetPasswordModel(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ApplicationDbContext applicationDbContext,
            ISmsSender smsSender):base(applicationDbContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailSender = emailSender;
            this._smsSender = smsSender;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();


            var user = await _userManager.FindByNameAsync(ForgetPasswordVM.PhoneNumber);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "برای این شماره تماس حساب کاربری وجود ندارد");
                return Page();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Page("ResetPassword",null, new { token, phoneNumber = user.PhoneNumber }, "Https");

            //await _emailSender.SendResetPasswodEmail(callback, user.Email);
            await _smsSender.Send($"برای بازیابی رمز عبور خود روی لینک زیر کلیک کنید\n {callback}",user.UserName);

            return RedirectToPage("ForgetPasswordConfirmation");
        }
    }
}
