using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Order
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<OrderMasterVM> OrderMaster { get; set; }
        public async Task OnGet(int pageIndex = 1, int pageSize = 10,string searchText="")
        {
            var totalRow = await _applicationDbContext.OrderMasters
                .CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            OrderMaster = await _applicationDbContext.OrderMasters
                .Include(i => i.User)
                .Where(w => (w.OrderNumber.ToString() + " " + w.TraceNumber).Contains(searchText))
                .OrderByDescending(o => o.SubmitDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new OrderMasterVM()
                {
                    ID = s.ID,
                    OrderNumber = s.OrderNumber,
                    SubmitDate = s.SubmitDate,
                    UserFullName = s.User.FirstName + " " + s.User.LastName,
                    Username = s.User.UserName,
                    OrderStatus = s.OrderStatus.GetDescription()
                })
                .ToListAsync();

        }
       
      
    }
}
