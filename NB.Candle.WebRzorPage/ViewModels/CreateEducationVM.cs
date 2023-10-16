using Microsoft.AspNetCore.Http;
using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateEducationVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Title { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Description { get; set; }
        [Display(Name = "فایل تصویری")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public IFormFile Image { get; set; }
        public FileType FileType { get; set; }
    }
}
