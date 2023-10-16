using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.OrderDiscount
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<OrderDiscountVM> OrderDiscounts { get; set; }
        public async Task OnGet(int pageIndex = 1, int pageSize = 10)
        {
            var totalRow = await _applicationDbContext.OrderDiscounts.CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            OrderDiscounts = await _applicationDbContext.OrderDiscounts
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new OrderDiscountVM()
                {
                    ID = s.ID,
                    FloorAmount = s.FloorAmount,
                    DiscountPersentage = s.DiscountPercentage,
                    IsActive = s.IsActive,
                })
                .ToListAsync();
        }
    }
}
