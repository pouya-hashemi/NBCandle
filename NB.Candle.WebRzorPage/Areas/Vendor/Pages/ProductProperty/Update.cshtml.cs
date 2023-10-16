using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ProductProperty
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
        public UpdateFixedProductPropertyVM Property { get; set; }
        public async Task OnGet(Guid Id)
        {
            var fixedProductPropertyModel = await _applicationDbContext.FixedProductProperties
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Property = _mapper.Map<UpdateFixedProductPropertyVM>(fixedProductPropertyModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var fixedProductPropertyModel = await _applicationDbContext.FixedProductProperties
               .FirstOrDefaultAsync(w => w.ID == Property.ID);

            fixedProductPropertyModel.ProductProperty.Height = Property.ProductProperty.Height;
            fixedProductPropertyModel.ProductProperty.Width = Property.ProductProperty.Width;
            fixedProductPropertyModel.ProductProperty.Diameter = Property.ProductProperty.Diameter;
            fixedProductPropertyModel.ProductProperty.Price = Property.ProductProperty.Price;
            fixedProductPropertyModel.IsActive = Property.IsActive;

            _applicationDbContext.FixedProductProperties.Update(fixedProductPropertyModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index",new { productId= fixedProductPropertyModel .ProductId});
        }
       
    }
}
