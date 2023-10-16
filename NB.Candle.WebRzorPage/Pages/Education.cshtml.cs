using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class EducationModel : BasePage
    {
        public EducationModel(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
        public IEnumerable<EducationVM> Educations{ get; set; }
        public async Task OnGet()
        {
            Educations = await _applicationDbContext.Educations
                .Where(w => w.IsActive)
                .Select(s => new EducationVM()
                {
                    ID=s.ID,
                    Title=s.Title,
                    FileType=s.FileType,
                    Description=s.Description,
                    ImageUrl=s.ImageUrl
                }).ToListAsync();
        }
    }
}
