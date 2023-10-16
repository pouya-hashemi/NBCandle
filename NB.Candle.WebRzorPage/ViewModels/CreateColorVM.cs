using Microsoft.AspNetCore.Http;
using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateColorVM
    {
        [Display(Name ="نام رنگ")]
        public string DisplayName { get; set; }
        [Display(Name ="کد رنگ")]
        public string ColorCode { get; set; }
        [Display(Name ="تصویر طرح")]
        public IFormFile Image { get; set; }
        [Display(Name ="نوع رنگ")]
        public ColorType ColorType { get; set; }
    }
}
