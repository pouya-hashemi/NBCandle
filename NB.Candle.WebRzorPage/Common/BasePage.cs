using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace NB.Candle.WebRzorPage.Common
{
    public class BasePage : PageModel
    {
        protected readonly ApplicationDbContext _applicationDbContext;

        public BasePage(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
            Categories = _applicationDbContext.Categories
                .Include(i => i.Childrens)
                .OrderBy(o => o.Sequence)
                .Select(s => new CategoryVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    ParentCategoryId = s.ParentCategoryId,
                    Childrens = s.Childrens.Select(s => new CategoryVM()
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Description = s.Description,
                        ImageUrl = s.ImageUrl
                    }),
                    IsActive=s.IsActive
                })
                .ToList();

            MoldCategories= _applicationDbContext.Categories
                .Include(i=>i.Products)
                 .Include(i => i.Childrens)
                .Where(w=>w.Products.Any(a=>a.ProductType==Data.Enums.ProductType.mold))
                .OrderBy(o => o.Sequence)
                .Select(s => new CategoryVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    ParentCategoryId = s.ParentCategoryId,
                    Childrens = s.Childrens.Select(s => new CategoryVM()
                    {
                        ID = s.ID,
                        Name = s.Name,
                        Description = s.Description,
                        ImageUrl = s.ImageUrl
                    }),
                    IsActive=s.IsActive
                })
                .ToList();
            var normalDiscountSlider = _applicationDbContext.NormalDiscounts
                .Where(w => w.IsActive)
                .Where(w => w.DisplayOnHomePage)
                .Select(s => new SliderVM()
                {
                    ImageUrl = s.ImageUrl,
                    Url = "/productDetail?productId=" + s.ProductId,
                    Title=s.Title,
                    Description=s.Description
                }).ToList();
            var defaultCategory = Categories.FirstOrDefault();
            var orderDiscountSlider = _applicationDbContext.OrderDiscounts
               .Where(w => w.IsActive)
               .Where(w => w.DisplayOnHomePage)
               .Select(s => new SliderVM()
               {
                   ImageUrl = s.ImageUrl,
                   Url = "/product" + (defaultCategory != null ? ("?categoryId=" + defaultCategory.ID) : ""),
                   Title = s.Title,
                   Description = s.Description
               }).ToList();

            Sliders = normalDiscountSlider.Concat(orderDiscountSlider);

        }
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<CategoryVM> MoldCategories { get; set; }
        public IEnumerable<SliderVM> Sliders { get; set; }=new List<SliderVM>();
        private string UserId=> HttpContext.User.Claims
                                    .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                                    .Select(s => s.Value)
                                    .FirstOrDefault();
        public int CartCount=> _applicationDbContext.Carts
            .Where(w => w.UserId == UserId)
            .Count();
    }
}
