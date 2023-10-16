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
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IWebHostEnvironment  hostingEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._hostingEnvironment = hostingEnvironment;
        }
        [BindProperty]
        public CreateCategoryVM CreateCategory { get; set; }
        public List<SelectListItem> CategoryListItems { get; set; }
        public async Task OnGet()
        {
            await FillSelectList();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Data.Models.Category>(CreateCategory);
                category.ID = Guid.NewGuid();

                var file = Path.Combine(_hostingEnvironment.WebRootPath, "Images","Category",$"Category_{category.ID}_{CreateCategory.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await CreateCategory.Image.CopyToAsync(fileStream);
                }
                category.ImageUrl = "/Images/Category/" + $"Category_{category.ID}_{CreateCategory.Image.FileName}";


                _applicationDbContext.Categories.Add(category);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index");
            }
            await FillSelectList();
            return Page();
        }

        private async Task FillSelectList()
        {
            CategoryListItems = await _applicationDbContext.Categories
               .Where(w => w.ParentCategoryId == null)
               .Select(s => new SelectListItem()
               {
                   Text = s.Name,
                   Value = s.ID.ToString()
               })
               .ToListAsync();
            CategoryListItems.Insert(0, new SelectListItem()
            {
                Value = "",
                Text = "بدون پدر"
            });
        }
    }
}
