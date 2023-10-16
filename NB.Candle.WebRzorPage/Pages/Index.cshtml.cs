using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.Services.Sms;
using NB.Candle.WebRzorPage.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Pages
{
    public class IndexModel : BasePage
    {
        private readonly ISmsSender _smsSender;
        private readonly SignInManager<User> _signInManager;

        public CategoryVM MainCategory { get; set; }
        public CategoryVM SecondCategory { get; set; }
        public CategoryVM ThirdCategory { get; set; }

        public IEnumerable<ProductDisplayVM> Offers { get; set; }
        public IEnumerable<ProductDisplayVM> BestSellers { get; set; }
        public IndexModel(ApplicationDbContext applicationDbContext,
            ISmsSender smsSender,
            SignInManager<User> signInManager) : base(applicationDbContext)
        {
            MainCategory = Categories.Where(w => w.ParentCategoryId == null).FirstOrDefault();
            SecondCategory = Categories.Where(w => w.ParentCategoryId == null).Skip(1).FirstOrDefault();
            ThirdCategory = Categories.Where(w => w.ParentCategoryId == null).Skip(2).FirstOrDefault();
            this._smsSender = smsSender;
            this._signInManager = signInManager;
        }

        public async Task OnGet()
        {
            //await _smsSender.Send("test", "9304056727");
            //throw new Exception("test");

            Offers = await _applicationDbContext.NormalDiscounts
                .Include(i => i.Product).ThenInclude(i => i.Category)
                .Include(i => i.Product).ThenInclude(i => i.FixedProductProperties).ThenInclude(i => i.ProductProperty)
                .Include(i => i.Product).ThenInclude(i => i.Images)
                .Where(w => w.IsActive)
                .Take(10)
                .OrderByDescending(o => o.InsertDateTime)
                .Select(s => new ProductDisplayVM()
                {

                    ID = s.ID,
                    Name = s.Product.Name,
                    CategoryName = s.Product.Category.Name,
                    ProductImages = s.Product.Images
                               .Where(w => w.IsActive)
                               .Select(m => new ProductImageVM()
                               {
                                   ID = m.ID,
                                   Url = m.Url,
                                   IsMain = m.IsMain
                               }).AsEnumerable(),

                    ProductProperties = s.Product.FixedProductProperties
                                   .Where(w => w.IsActive)
                                   .Select(p => new FixedProductPropertyVM()
                                   {
                                       ID = p.ID,
                                       Height = p.ProductProperty.Height,
                                       Price = p.ProductProperty.Price,
                                       Width = p.ProductProperty.Width,
                                       Diameter = p.ProductProperty.Diameter,
                                   }).AsEnumerable(),
                    NormalDiscount =
                     new NormalDiscountVM()
                     {
                         ID = s.ID,
                         DiscountPercentage = s.DiscountPercentage
                     }

                }).ToListAsync();


            BestSellers = await _applicationDbContext.Products
                .Include(i => i.Category)
                .Include(i => i.FixedProductProperties).ThenInclude(i => i.ProductProperty)
                .Include(i => i.Images)
                .Where(w => w.IsActive)
                .Take(10)
                .OrderByDescending(o => o.OrderCount)
                .Select(s => new ProductDisplayVM()
                {

                    ID = s.ID,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    ProductImages = s.Images
                               .Where(w => w.IsActive)
                               .Select(m => new ProductImageVM()
                               {
                                   ID = m.ID,
                                   Url = m.Url,
                                   IsMain = m.IsMain
                               }).AsEnumerable(),

                    ProductProperties = s.FixedProductProperties
                                   .Where(w => w.IsActive)
                                   .Select(p => new FixedProductPropertyVM()
                                   {
                                       ID = p.ID,
                                       Height = p.ProductProperty.Height,
                                       Price = p.ProductProperty.Price,
                                       Width = p.ProductProperty.Width,
                                       Diameter = p.ProductProperty.Diameter,
                                   }).AsEnumerable(),
                    NormalDiscount =
                     s.NormalDiscounts.Where(w => w.IsActive)
                    .OrderByDescending(o => o.InsertDateTime)
                    .Select(d => new NormalDiscountVM()
                    {
                        ID = d.ID,
                        DiscountPercentage = d.DiscountPercentage
                    })
                    .FirstOrDefault()
                }).ToListAsync();

        }
        public async Task<JsonResult> OnGetProductSearch(string searchString)
        {
            var result = await _applicationDbContext.Products
                 .Include(i => i.Category)
                 .Where(w => (w.Name + " " + w.Category.Name).Contains(searchString))
                 .Select(s => new ProductSearchVM { ID = s.ID, Name = s.Name, Category = s.Category.Name })
                 .ToListAsync();

            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/index");
        }
    }
}
