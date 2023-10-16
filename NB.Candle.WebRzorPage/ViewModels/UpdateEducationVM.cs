using Microsoft.AspNetCore.Http;
using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateEducationVM
    {
        public Guid ID { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Title { get; set; }
        [Display(Name = "متن")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Description { get; set; }
        [Display(Name = "فایل تصویری")]
        public IFormFile Image { get; set; }
        public FileType FileType { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
