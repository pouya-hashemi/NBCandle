using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ProductProperty
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
        public CreateFixedProductPropertyVM Property { get; set; }
        public void  OnGet(Guid productId)
        {
            Property = new CreateFixedProductPropertyVM() { ProductId = productId };
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var fixedProductProperty = _mapper.Map<FixedProductProperty>(Property);
                fixedProductProperty.ID = Guid.NewGuid();
                fixedProductProperty.IsActive = true;

                _applicationDbContext.FixedProductProperties.Add(fixedProductProperty);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index",new { productId=Property.ProductId});
            }
            return Page();
        }
    }
}
