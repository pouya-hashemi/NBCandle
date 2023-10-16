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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<ProductVM> Products { get; set; }
        public async Task OnGet(int pageIndex = 1, int pageSize = 10)
        {
            var totalRow = await _applicationDbContext.Products.CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            Products = await _applicationDbContext.Products
                .Include(i=>i.Category)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ProductVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    CategoryName=s.Category.Name,
                    IsActive=s.IsActive,
                    IsFreeSize=s.IsFreeSize,

                })
                .ToListAsync();
        }
    }
}
