using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Product
{
    public class ColorsModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ColorsModel(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }
        public IEnumerable<ColorProductVM> ColorProduct { get; set; }
        [BindProperty]
        public string SelectedIds { get; set; }
        public async Task OnGet(Guid productId)
        {
            ColorProduct = await _applicationDbContext.Colors
                .Where(w => w.IsActive)
                .Select(s => new ColorProductVM()
                {
                    ID = s.ID,
                    ColorType = s.ColorType,
                    ColorCode = s.ColorCode,
                    ImageUrl = s.ImageUrl,
                })
                .ToListAsync();

            var product = await _applicationDbContext.Products
                .Include(i => i.Colors)
                .Where(w => w.ID == productId)
                .FirstOrDefaultAsync();

            ColorProduct.ToList().ForEach(f => f.IsSelected = product.Colors.Any(a => a.ID == f.ID));
            product.Colors.ToList().ForEach(f => SelectedIds += f.ID.ToString() + ",");

        }
        public async Task<IActionResult> OnPost(Guid productId)
        {
            var ids = SelectedIds.Split(',');
            var product = await _applicationDbContext.Products
                    .Include(i => i.Colors)
                    .Where(w => w.ID == productId)
                    .FirstOrDefaultAsync();
            
            var colors = new List<Color>();
            foreach (var id in ids)
            {
                if (id != "")
                {
                    colors.Add(await _applicationDbContext.Colors.FirstOrDefaultAsync(f=>f.ID==Guid.Parse(id)));
                }
            }
            product.Colors = colors;
            _applicationDbContext.Products.Update(product);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("index");
        }
    }
}
