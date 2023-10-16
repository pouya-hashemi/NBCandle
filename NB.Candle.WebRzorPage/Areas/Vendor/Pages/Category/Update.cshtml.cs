using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Category
{
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public UpdateCategoryVM Category { get; set; }
        public List<SelectListItem> CategoryListItems { get; set; }
        public async Task OnGet(Guid Id)
        {
            await FillSelectList(Id);
            var categoryModel = await _applicationDbContext.Categories
                .Include(i => i.ParentCategory)
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Category = _mapper.Map<UpdateCategoryVM>(categoryModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await FillSelectList(Category.ID);
                return Page();
            }
            var categoryModel = await _applicationDbContext.Categories
               .FirstOrDefaultAsync(w => w.ID == Category.ID);

            categoryModel.Name = Category.Name;
            categoryModel.Description = Category.Description;
            categoryModel.Sequence = Category.Sequence;
            categoryModel.ParentCategoryId = Category.ParentCategoryId;
            categoryModel.IsActive = Category.IsActive;
            if (Category.Image != null)
            {

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Category", $"Category_{categoryModel.ID}_{Category.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Category.Image.CopyToAsync(fileStream);
                }
                var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + categoryModel.ImageUrl.Replace('/', '\\');
                FileInfo oldFile = new FileInfo(path);
                if (oldFile.Exists)
                {
                    oldFile.Delete();
                }

                categoryModel.ImageUrl = "/Images/Category/" + $"Category_{categoryModel.ID}_{Category.Image.FileName}";
            }


            _applicationDbContext.Categories.Update(categoryModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
        private async Task FillSelectList(Guid id)
        {
            CategoryListItems = await _applicationDbContext.Categories
               .Where(w => w.ParentCategoryId == null
               && w.ID != id)
               .Select(s => new SelectListItem()
               {
                   Text = s.Name,
                   Value = s.ID.ToString()
               })
               .ToListAsync();
            CategoryListItems.Insert(0, new SelectListItem()
            {
                Value = "",
                Text = "»œÊ‰ Åœ—"
            });
        }
    }
}
