using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.OrderDiscount
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public CreateOrderDiscountVM Discount { get; set; }
        public void  OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var discount = _mapper.Map<Data.Models.OrderDiscount>(Discount);
                discount.ID = Guid.NewGuid();
                discount.IsActive = true;

                if (Discount.Image != null)
                {
                    var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Discount", $"Discount_{discount.ID}_{Discount.Image.FileName}");
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Discount.Image.CopyToAsync(fileStream);
                    }
                    discount.ImageUrl = "/Images/Discount/" + $"Discount_{discount.ID}_{Discount.Image.FileName}";
                }

                _applicationDbContext.OrderDiscounts.Add(discount);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index");
            }
            return Page();
        }
    }
}
