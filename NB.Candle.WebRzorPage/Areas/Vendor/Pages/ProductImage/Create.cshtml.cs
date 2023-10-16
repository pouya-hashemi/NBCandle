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

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.ProductImage
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
        public CreateProductImageVM Image { get; set; }
        public void OnGet(Guid productId)
        {
            Image = new CreateProductImageVM() { ProductId = productId };
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var productImage = _mapper.Map<Data.Models.ProductImage>(Image);
                productImage.ID = Guid.NewGuid();
                productImage.IsActive = true;

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Product", $"Product{productImage.ID}_{Image.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Image.Image.CopyToAsync(fileStream);
                }
                productImage.Url = "/Images/Product/" + $"Product{productImage.ID}_{Image.Image.FileName}";


                var imageCount = await _applicationDbContext.ProductImages
                        .Where(w => w.ProductId == productImage.ProductId).CountAsync();
                if (imageCount == 0)
                {
                    productImage.IsMain = true;
                }

                if (productImage.IsMain && imageCount > 0)
                {
                    var ProductImages = await _applicationDbContext.ProductImages
                        .Where(w => w.ProductId == productImage.ProductId)
                        .ToListAsync();
                    ProductImages.ForEach(f => f.IsMain = false);
                    _applicationDbContext.ProductImages.UpdateRange(ProductImages);
                    await _applicationDbContext.SaveChangesAsync();
                }

                _applicationDbContext.ProductImages.Add(productImage);
                await _applicationDbContext.SaveChangesAsync();

                return RedirectToPage("index", new { productId = Image.ProductId });
            }
            return Page();
        }
    }

}
