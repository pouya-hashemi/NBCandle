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
        public UpdateProductImageVM Image { get; set; }

        public async Task OnGet(Guid Id)
        {
            var productImageModel = await _applicationDbContext.ProductImages
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Image = _mapper.Map<UpdateProductImageVM>(productImageModel);

        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var productImageModel = await _applicationDbContext.ProductImages
               .FirstOrDefaultAsync(w => w.ID == Image.ID);

            var ProductImages = await _applicationDbContext.ProductImages
                    .Where(w => w.ProductId == productImageModel.ProductId)
                    .ToListAsync();
            if (Image.IsMain)
            {

                ProductImages.ForEach(f => f.IsMain = false);
                _applicationDbContext.ProductImages.UpdateRange(ProductImages);
                await _applicationDbContext.SaveChangesAsync();
            }
            else
            {
                if (productImageModel.IsMain)
                {
                    var newMainProductImage = ProductImages.FirstOrDefault(f => f.ID != Image.ID);
                    if (newMainProductImage != null)
                    {

                        newMainProductImage.IsMain = true;
                        _applicationDbContext.ProductImages.Update(newMainProductImage);
                        await _applicationDbContext.SaveChangesAsync();
                    }
                }
            }


            productImageModel.IsMain = Image.IsMain;
            productImageModel.IsActive = Image.IsActive;


            if (Image.Image != null)
            {

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Product", $"Product{Image.ID}_{Image.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Image.Image.CopyToAsync(fileStream);
                }
                var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + productImageModel.Url.Replace('/', '\\');
                FileInfo oldFile = new FileInfo(path);
                if (oldFile.Exists)//check file exsit or not  
                {
                    oldFile.Delete();
                }
                productImageModel.Url = "/Images/Product/" + $"Product{Image.ID}_{Image.Image.FileName}";
            }


            _applicationDbContext.ProductImages.Update(productImageModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index", new { productId = productImageModel.ProductId });
        }

    }
}
