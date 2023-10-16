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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ShippingMethod
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<CartShippingMethodVM> ShippingMethods { get; set; }
        public async Task OnGet(int pageIndex = 1, int pageSize = 10)
        {
            var totalRow = await _applicationDbContext.ShippingMethods
                .CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            ShippingMethods = await _applicationDbContext.ShippingMethods
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new CartShippingMethodVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Cost = s.Cost,
                    IsInInvoice = s.IsInInvoice,
                    IsActive = s.IsActive,
                    IsDefault=s.IsDefault,
                    EstimatedDaysToDeliver=s.EstimatedDaysToDeliver
                })
                .ToListAsync(); 
        }
    }
}
