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
    public class AccountModel : BasePage
    {
        private readonly UserManager<User> _userManager;

        public AccountModel(ApplicationDbContext applicationDbContext,
            UserManager<User> userManager):base(applicationDbContext)
        {
            this._userManager = userManager;
        }
        [BindProperty]
        public UpdateAccountVM Account{ get; set; }
        public async Task OnGet()
        {
            var userId = HttpContext.User.Claims
                           .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                           .Select(s => s.Value)
                           .FirstOrDefault();

            var user = await _userManager.FindByIdAsync(userId);
            Account = new UpdateAccountVM()
            {
                ID = user.Id,
                Address = user.UserAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PostalCode = user.PostalCode
            };

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var userId = HttpContext.User.Claims
                          .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                          .Select(s => s.Value)
                          .FirstOrDefault();

            var user = await _userManager.FindByIdAsync(userId);


            user.UserAddress = Account.Address;
            user.FirstName = Account.FirstName;
            user.LastName = Account.LastName;
            user.PostalCode = Account.PostalCode;
            await _userManager.UpdateAsync(user);

            return RedirectToPage("index");

        }
    }
}
