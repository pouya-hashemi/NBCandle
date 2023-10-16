using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ProductProperty
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<FixedProductPropertyVM> Properties { get; set; }
        public ProductVM Product { get; set; }
        public async Task OnGet(Guid ProductId, int pageIndex = 1, int pageSize = 10)
        {
            var totalRow = await _applicationDbContext.FixedProductProperties
                .Where(w => w.ProductId == ProductId)
                .CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            Properties = await _applicationDbContext.FixedProductProperties
                .Where(w => w.ProductId == ProductId)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new FixedProductPropertyVM()
                {
                    ID = s.ID,
                    Height = s.ProductProperty.Height,
                    Width = s.ProductProperty.Width,
                    Diameter = s.ProductProperty.Diameter,
                    Price = s.ProductProperty.Price,
                    IsActive=s.IsActive
                })
                .ToListAsync();

            Product = await _applicationDbContext.Products
                 .Include(i => i.Category)
                 .Where(w => w.ID == ProductId)
                 .Select(s=>new ProductVM() { 
                 ID=s.ID,
                 Name=s.Name,
                 CategoryName=s.Category.Name
                 })
                 .FirstOrDefaultAsync();

        }
    }
}

