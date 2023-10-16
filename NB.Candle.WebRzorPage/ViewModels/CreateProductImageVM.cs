using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateProductImageVM
    {
        
        public Guid ProductId { get; set; }
        [Display(Name = "تصویر کالا")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public IFormFile Image { get; set; }
        [Display(Name = "تصویر اصلی")]
        public bool IsMain { get; set; }
    }
}
