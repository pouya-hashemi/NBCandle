using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Colors
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public ColorVM Color { get; set; }
        public async Task OnGet(Guid Id)
        {
            var colorModel = await _applicationDbContext.Colors
               .Where(w => w.ID == Id)
               .FirstOrDefaultAsync();

            Color = _mapper.Map<ColorVM>(colorModel);
        }
        public async Task<IActionResult> OnPost()
        {
            var colorModel = await _applicationDbContext.Colors
                .Include(i => i.Products)
                .Where(w => w.ID == Color.ID)
                .FirstOrDefaultAsync();
            if (colorModel.Products.Any())
            {
                ModelState.AddModelError(string.Empty, "این آیتم دارای فرزند می باشد. امکان حذف وجود ندارد.");
                Color = _mapper.Map<ColorVM>(colorModel);
                return Page();
            }
            if (colorModel.ColorType == Data.Enums.ColorType.pattern)
            {
                var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + colorModel.ImageUrl.Replace('/', '\\');
                FileInfo oldFile = new FileInfo(path);
                if (oldFile.Exists)
                {
                    oldFile.Delete();
                }
            }
            _applicationDbContext.Colors.Remove(colorModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
