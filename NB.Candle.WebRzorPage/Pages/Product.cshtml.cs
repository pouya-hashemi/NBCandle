using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Enums;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class ProductModel : BasePage
    {
        public ProductModel(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
        public IEnumerable<ProductDisplayVM> ProductDisplays { get; set; }
        public PageinateDetail Pageinate { get; set; }
        public CategoryVM Category { get; set; }
        public async Task OnGet(Guid? categoryId,ProductType productType=0, int pageIndex = 1, int pageSize = 10)
        {
            Category = await _applicationDbContext.Categories
                .Where(w=>w.ID==categoryId || categoryId==null)
                .Select(s=>new CategoryVM() { 
                ID=s.ID,
                Name=s.Name,
                Description=s.Description,
                })
                .FirstOrDefaultAsync();
            var totalRow = await _applicationDbContext.Products
                 .Where(w => w.IsActive)
                .Where(w => w.CategoryId == categoryId)
                .CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);

            ProductDisplays =await _applicationDbContext.Products
                .Include(i => i.Category)
                .Include(i => i.FixedProductProperties).ThenInclude(i => i.ProductProperty)
                .Include(i => i.Images)
                .Include(i => i.NormalDiscounts)
                .Where(w => w.IsActive)
                .Where(w=>w.CategoryId==categoryId)
                .Where(w => w.ProductType == productType)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
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
                    NormalDiscount=s.NormalDiscounts.Where(w=>w.IsActive)
                    .OrderByDescending(o=>o.InsertDateTime)
                    .Select(d=>new NormalDiscountVM() { 
                    ID=d.ID,
                    DiscountPercentage=d.DiscountPercentage
                    })
                    .FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}
