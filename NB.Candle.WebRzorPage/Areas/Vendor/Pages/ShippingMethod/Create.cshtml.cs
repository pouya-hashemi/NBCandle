using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ShippingMethod
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
        public CreateShippingMethodVM ShippingMethod { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var shippingMethod = _mapper.Map<Data.Models.ShippingMethod>(ShippingMethod);
                shippingMethod.ID = Guid.NewGuid();
                shippingMethod.IsActive = true;

                _applicationDbContext.ShippingMethods.Add(shippingMethod);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
