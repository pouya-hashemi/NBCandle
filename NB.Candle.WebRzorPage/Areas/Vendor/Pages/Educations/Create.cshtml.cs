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
using NB.Candle.WebRzorPage.Data.Enums;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Educations
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
        public CreateEducationVM Education { get; set; }
        public List<SelectListItem> FileTypeItems { get; set; }
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
            if (Education.Image.Length == 0)
            {
                ModelState.AddModelError(string.Empty, "انتخاب فایل الزامیست");
                return Page();
            }
            var education = _mapper.Map<Education>(Education);
            education.ID = Guid.NewGuid();
            education.IsActive = true;

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Education", $"Education{education.ID}_{Education.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Education.Image.CopyToAsync(fileStream);
                }
                education.ImageUrl = "/Images/Education/" + $"Education{education.ID}_{Education.Image.FileName}";

            _applicationDbContext.Educations.Add(education);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("index");

        }
        private void FillSelectList()
        {
            FileTypeItems = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "فیلم",
                   Value = "0"
                },
                new SelectListItem()
                {
                    Text = "عکس",
                   Value = "1"
                },
            };

        }
    }
}
