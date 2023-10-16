using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CreateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        [BindProperty]
        public CreateProductVM Product { get; set; }
        public List<SelectListItem> CategoryListItems { get; set; }
        public List<SelectListItem> ProductTypeListItems { get; set; }
        public async Task OnGet()
        {
            await FillSelectList();
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Data.Models.Product>(Product);
                product.ID = Guid.NewGuid();
                product.IsActive = true;

                _applicationDbContext.Products.Add(product);
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

            ProductTypeListItems = new List<SelectListItem>()
            {
                new SelectListItem(){
                    Text="‘„⁄",
                    Value="0"
                },
                new SelectListItem(){
                    Text="ﬁ«·»",
                    Value="1"
                }
            };


        }
    }
}
