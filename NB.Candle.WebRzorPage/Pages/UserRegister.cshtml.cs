using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.Services.Sms;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class UserRegisterModel : BasePage
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly ISmsSender _smsSender;

        [BindProperty]
        public UserVM UserVM { get; set; }

        public UserRegisterModel(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper,
            ApplicationDbContext applicationDbContext,
            ISmsSender smsSender):base(applicationDbContext)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._mapper = mapper;
            this._smsSender = smsSender;
        }
        public void OnGet(string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
        }
        public async Task<IActionResult> OnPost(string returnUrl=null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (!(UserVM.Username.StartsWith("09") && UserVM.Username.Length==11) &&!(UserVM.Username.StartsWith("9") && UserVM.Username.Length == 10))
            {
                ModelState.AddModelError(string.Empty, "شماره تماس صحیح نیست");
                return Page();
            }
            if (UserVM.Password != UserVM.PasswordReEnter)
            {
                ModelState.AddModelError(string.Empty, "تکرار رمز با رمز عبور همخوانی ندارد");
                return Page();
            }
            var userEixst = await _userManager.FindByNameAsync(UserVM.Username);
            if (userEixst != null)
            {
                ModelState.AddModelError(string.Empty, "برای این شماره تماس حساب کاربری وجود دارد");
                return Page();
            }
            if (ModelState.IsValid)
            {
                var Otp = new UserOtp()
                {
                    ID = Guid.NewGuid(),
                    OtpCode = new Random().Next(10000, 99999).ToString(),
                    ExpireDate = DateTime.UtcNow.AddMinutes(5),
                    FirstName=UserVM.FirstName,
                    LastName=UserVM.LastName,
                    Username=UserVM.Username.StartsWith("0")?UserVM.Username.Substring(1):UserVM.Username,
                    Password=UserVM.Password,
                    PasswortReEnter=UserVM.PasswordReEnter,
                };
                _applicationDbContext.UserOtps.Add(Otp);
                await _applicationDbContext.SaveChangesAsync();

                await _smsSender.SendVerification(UserVM.Username,Otp.OtpCode);

                return RedirectToPage("ConfirmNumber", new { id=Otp.ID,returnUrl=returnUrl});
            }
            return Page();
        }
    }
}
