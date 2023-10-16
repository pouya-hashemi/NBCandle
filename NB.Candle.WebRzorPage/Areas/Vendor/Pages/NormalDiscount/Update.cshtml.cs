using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.NormalDiscount
{
    public class UpdateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UpdateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public UpdateNormalDiscountVM Discount { get; set; }
        public async Task OnGet(Guid Id)
        {
            var normalDiscountModel = await _applicationDbContext.NormalDiscounts
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Discount = _mapper.Map<UpdateNormalDiscountVM>(normalDiscountModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var normalDiscountModel = await _applicationDbContext.NormalDiscounts
               .FirstOrDefaultAsync(w => w.ID == Discount.ID);

            normalDiscountModel.DiscountPercentage = Discount.DiscountPercentage;
            normalDiscountModel.IsActive = Discount.IsActive;
            normalDiscountModel.DisplayOnHomePage = Discount.DisplayOnHomePage;
            normalDiscountModel.Title = Discount.Title;
            normalDiscountModel.Description = Discount.Description;

            if (Discount.Image!=null)
            {

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Discount", $"Discount_{normalDiscountModel.ID}_{Discount.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Discount.Image.CopyToAsync(fileStream);
                }
                normalDiscountModel.ImageUrl = "/Images/Discount/" + $"Discount_{normalDiscountModel.ID}_{Discount.Image.FileName}";
            }

            _applicationDbContext.NormalDiscounts.Update(normalDiscountModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index", new { productId = normalDiscountModel.ProductId });
        }
    }
}
