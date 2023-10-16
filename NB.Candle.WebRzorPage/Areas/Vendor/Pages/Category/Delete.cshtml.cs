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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Category
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
        public CategoryVM Category { get; set; }
        public async Task OnGet(Guid Id)
        {
            var categoryModel = await _applicationDbContext.Categories
               .Include(i => i.ParentCategory)
               .Where(w => w.ID == Id)
               .FirstOrDefaultAsync();

            Category = _mapper.Map<CategoryVM>(categoryModel);
        }
        public async Task<IActionResult> OnPost()
        {
            var categoryModel = await _applicationDbContext.Categories
                .Include(i => i.Childrens)
                .Include(i=>i.ParentCategory)
                .Where(w => w.ID == Category.ID)
                .FirstOrDefaultAsync();
            if (categoryModel.Childrens.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "این آیتم دارای فرزند می باشد. امکان حذف وجود ندارد.");
                Category = _mapper.Map<CategoryVM>(categoryModel);
                return Page();
            }
            var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + categoryModel.ImageUrl.Replace('/', '\\');
            FileInfo oldFile = new FileInfo(path);
            if (oldFile.Exists) 
            {
                oldFile.Delete();
            }
            _applicationDbContext.Categories.Remove(categoryModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
