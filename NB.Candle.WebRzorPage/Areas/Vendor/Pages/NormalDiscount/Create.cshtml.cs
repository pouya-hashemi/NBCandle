using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.NormalDiscount
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
        public CreateNormalDiscountVM Discount { get; set; }
        public void OnGet(Guid productId)
        {
            Discount = new CreateNormalDiscountVM() { ProductId = productId };
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var normalDiscount = _mapper.Map<Data.Models.NormalDiscount>(Discount);
                normalDiscount.ID = Guid.NewGuid();
                normalDiscount.IsActive = true;
                normalDiscount.InsertDateTime = DateTime.UtcNow;

                if (Discount.Image !=null)
                {

                    var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Discount", $"Discount_{normalDiscount.ID}_{Discount.Image.FileName}");
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await Discount.Image.CopyToAsync(fileStream);
                    }
                    normalDiscount.ImageUrl = "/Images/Discount/" + $"Discount_{normalDiscount.ID}_{Discount.Image.FileName}";
                }

                _applicationDbContext.NormalDiscounts.Add(normalDiscount);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index", new { productId = Discount.ProductId });
            }
            return Page();
        }
    }
}
