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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Colors
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate { get; set; }
        public IEnumerable<ColorVM> Colors { get; set; }
        public async Task OnGet(int pageIndex = 1, int pageSize = 10)
        {
            var totalRow = await _applicationDbContext.Colors
                .CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            Colors = await _applicationDbContext.Colors
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new ColorVM()
                {
                    ID = s.ID,
                    DisplayName = s.DisplayName,
                    ColorType = s.ColorType,
                    ColorTypeName = s.ColorType.GetDescription(),
                    ColorCode = s.ColorCode,
                    ImageUrl = s.ImageUrl,
                    IsActive = s.IsActive
                })
                .ToListAsync();
        }
    }
}
