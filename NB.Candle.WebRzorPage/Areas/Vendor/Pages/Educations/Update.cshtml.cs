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
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Educations
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
        public UpdateEducationVM Education { get; set; }
        public List<SelectListItem> FileTypeListItems { get; set; }
        public async Task OnGet(Guid Id)
        {
             FillSelectList(Id);
            var educationModel = await _applicationDbContext.Educations
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            Education = _mapper.Map<UpdateEducationVM>(educationModel);

        }
        
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                 FillSelectList(Education.ID);
                return Page();
            }
            var educationModel = await _applicationDbContext.Educations
               .FirstOrDefaultAsync(w => w.ID == Education.ID);

            educationModel.Title = Education.Title;
            educationModel.Description = Education.Description;
            educationModel.FileType = Education.FileType;
            educationModel.IsActive = Education.IsActive;
            if (Education.Image!=null&& Education.Image.Length>0)
            {

                var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Education", $"Education_{educationModel.ID}_{Education.Image.FileName}");
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Education.Image.CopyToAsync(fileStream);
                }
                var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + educationModel.ImageUrl.Replace('/', '\\');
                FileInfo oldFile = new FileInfo(path);
                if (oldFile.Exists)
                {
                    oldFile.Delete();
                }

                educationModel.ImageUrl = "/Images/Education/" + $"Education_{educationModel.ID}_{Education.Image.FileName}";
            }


            _applicationDbContext.Educations.Update(educationModel);
            await _applicationDbContext.SaveChangesAsync();
            return RedirectToPage("index");
        }
        private void FillSelectList(Guid id)
        {
            FileTypeListItems = new List<SelectListItem>()
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
