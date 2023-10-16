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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.OrderDiscount
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
        public UpdateOrderDiscountVM Discount { get; set; }
        public async Task OnGet(Guid Id)
        {
            var discountModel = await _applicationDbContext.OrderDiscounts
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Discount = _mapper.Map<UpdateOrderDiscountVM>(discountModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var discountModel = await _applicationDbContext.OrderDiscounts
                .Where(w => w.ID == Discount.ID)
                .FirstOrDefaultAsync();

            discountModel.FloorAmount = Discount.FloorAmount;
            discountModel.DiscountPercentage = Discount.DiscountPercentage;
            discountModel.IsActive = Discount.IsActive;
            discountModel.DisplayOnHomePage = Discount.DisplayOnHomePage;
            discountModel.Title = Discount.Title;
            discountModel.Description = Discount.Description;

            if (Discount.Image != null)
            {
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Discount", $"Discount_{discountModel.ID}_{Discount.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Discount.Image.CopyToAsync(fileStream);
                }
                discountModel.ImageUrl = "/Images/Discount/" + $"Discount_{discountModel.ID}_{Discount.Image.FileName}";
            }


            _applicationDbContext.OrderDiscounts.Update(discountModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
    }
}
