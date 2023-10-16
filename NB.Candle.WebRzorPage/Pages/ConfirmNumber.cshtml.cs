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
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class ConfirmNumberModel : BasePage
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public ConfirmNumberModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            UserManager<User> userManager,
            SignInManager<User> signInManager) : base(applicationDbContext)
        {
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        [BindProperty]
        public UserOtpVM UserOtpVM { get; set; }
        public void OnGet(Guid id, string returnUrl)
        {
            ViewData["returnUrl"] = returnUrl;
            UserOtpVM = new UserOtpVM()
            {
                ID = id
            };
        }
        public async Task<IActionResult> OnPost(string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            var otp = _applicationDbContext.UserOtps
                .Where(w => w.ID == UserOtpVM.ID)
                .FirstOrDefault();

            if (otp == null)
            {
                ModelState.AddModelError(string.Empty, "گد شما یافت نشد");
                return Page();
            }
            if (otp.OtpCode != UserOtpVM.OtpCode)
            {
                ModelState.AddModelError(string.Empty, "گد وارد شده صحبح نیست");
                return Page();
            }
            if (otp.ExpireDate < DateTime.UtcNow)
            {
                ModelState.AddModelError(string.Empty, "گد شما منقضی شده! لطفا مجدد تلاش کنید");
                return Page();
            }
            var user = new User()
            {
                FirstName = otp.FirstName,
                LastName = otp.LastName,
                UserName = otp.Username,
                PhoneNumber = otp.Username,
            };

            var result = await _userManager.CreateAsync(user, otp.Password);
            if (result.Succeeded)
            {
                var cartinfo = new CartInfo()
                {
                    ID = Guid.NewGuid(),
                    ShippingMethodId = _applicationDbContext.ShippingMethods.Where(w => w.IsDefault).Select(s => s.ID).FirstOrDefault(),
                    UserId=user.Id
                };
                _applicationDbContext.CartInfos.Add(cartinfo);
                await _applicationDbContext.SaveChangesAsync();
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect(returnUrl);

            }
            foreach (var error in result.Errors)
            {
                if (error.Code == "InvalidUserName")
                {
                    ModelState.AddModelError(string.Empty, "این نام کاربری قابل استفاده نیست");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }



            return Page();
        }
    }
}
