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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteModel(ApplicationDbContext applicationDbContext,
            IMapper mapper,
            IWebHostEnvironment  webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._mapper = mapper;
            this._webHostEnvironment = webHostEnvironment;
        }
        [BindProperty]
        public ProductImageVM Image { get; set; }
        public async Task OnGet(Guid Id)
        {
            var productImageModel = await _applicationDbContext.ProductImages
               .Where(w => w.ID == Id)
               .FirstOrDefaultAsync();

            Image = _mapper.Map<ProductImageVM>(productImageModel);
        }
        public async Task<IActionResult> OnPost()
        {
            
            var productImageModel = await _applicationDbContext.ProductImages
                .Where(w => w.ID == Image.ID)
                .FirstOrDefaultAsync();
            var productId = productImageModel.ProductId;

            if (productImageModel.IsMain)
            {
                var newMainProductImage = _applicationDbContext.ProductImages.FirstOrDefault(f => f.ID != Image.ID);
                if (newMainProductImage != null)
                {

                    newMainProductImage.IsMain = true;
                    _applicationDbContext.ProductImages.Update(newMainProductImage);
                    await _applicationDbContext.SaveChangesAsync();
                }
            }
            var path = _webHostEnvironment.ContentRootPath+"\\wwwroot" + productImageModel.Url.Replace('/','\\');
            FileInfo oldFile = new FileInfo(path);
            if (oldFile.Exists)//check file exsit or not  
            {
                oldFile.Delete();
            }

            _applicationDbContext.ProductImages.Remove(productImageModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index",new { ProductId= productId });
        }
    }
}
