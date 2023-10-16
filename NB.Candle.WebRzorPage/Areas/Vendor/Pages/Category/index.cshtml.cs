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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Category.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IndexModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public PageinateDetail Pageinate{ get; set; }
        public IEnumerable<CategoryVM> Categories{ get; set; }
        public async Task OnGet(int pageIndex=1,int pageSize=10)
        {
            var totalRow=await _applicationDbContext.Categories.CountAsync();
            Pageinate = new PageinateDetail(pageIndex, pageSize, totalRow);


            Categories = await _applicationDbContext.Categories
                .Include(i => i.ParentCategory)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new CategoryVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    ParentName = s.ParentCategory.Name,
                    Description=s.Description,
                    ImageUrl=s.ImageUrl,
                    Sequence=s.Sequence,
                    IsActive=s.IsActive
                })
                .ToListAsync();
        }
    }
}
