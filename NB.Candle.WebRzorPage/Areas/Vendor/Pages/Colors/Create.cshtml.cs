using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Colors
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
        public CreateColorVM Color { get; set; }
        public List<SelectListItem> ColorTypeItems { get; set; }
        public void OnGet()
        {
            FillSelectList();
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                FillSelectList();
                return Page();
            }
            var color = _mapper.Map<Data.Models.Color>(Color);
            color.ID = Guid.NewGuid();
            color.IsActive = true;

            if (Color.ColorType == Data.Enums.ColorType.pattern)
            {
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Color", $"Color{color.ID}_{Color.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Color.Image.CopyToAsync(fileStream);
                }
                color.ImageUrl = "/Images/Color/" + $"Color{color.ID}_{Color.Image.FileName}";
            }

            _applicationDbContext.Colors.Add(color);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("index");

        }
        private void FillSelectList()
        {
            ColorTypeItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "—‰ê",
                   Value = "0"
                },
                new SelectListItem()
                {
                    Text = "ÿ—Õ",
                   Value = "1"
                },
            };

        }
    }
}
