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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ShippingMethod
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
        public UpdateShippingMethodVM ShippingMethod { get; set; }
        public async Task OnGet(Guid Id)
        {
            var ShippingMethodModel = await _applicationDbContext.ShippingMethods
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            ShippingMethod = _mapper.Map<UpdateShippingMethodVM>(ShippingMethodModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var shippingMethodModel = await _applicationDbContext.ShippingMethods
               .FirstOrDefaultAsync(w => w.ID == ShippingMethod.ID);

            shippingMethodModel.Name = ShippingMethod.Name;
            shippingMethodModel.Cost = ShippingMethod.Cost;
            shippingMethodModel.EstimatedDaysToDeliver = ShippingMethod.EstimatedDaysToDeliver;
            shippingMethodModel.IsInInvoice = ShippingMethod.IsInInvoice;
            shippingMethodModel.IsActive = ShippingMethod.IsActive;
            shippingMethodModel.IsDefault = ShippingMethod.IsDefault;

            _applicationDbContext.ShippingMethods.Update(shippingMethodModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
