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
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;


        public UpdateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
        }
        [BindProperty]
        public UpdateProductVM Product { get; set; }
        public List<SelectListItem> CategoryListItems { get; set; }
        public List<SelectListItem> ProductTypeListItems { get; set; }
        public async Task OnGet(Guid Id)
        {
            await FillSelectList(Id);
            var productModel = await _applicationDbContext.Products
                .Include(i => i.Category)
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Product = _mapper.Map<UpdateProductVM>(productModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await FillSelectList(Product.ID);
                return Page();
            }
            var productModel = await _applicationDbContext.Products
               .FirstOrDefaultAsync(w => w.ID == Product.ID);

            productModel.Name = Product.Name;
            productModel.ProductType = Product.ProductType;
            productModel.CategoryId = Product.CategoryId;
            productModel.IsActive = Product.IsActive;
            productModel.IsFreeSize = Product.IsFreeSize;
            
            _applicationDbContext.Products.Update(productModel);
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
