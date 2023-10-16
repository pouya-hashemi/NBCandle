using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateProductImageVM
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        [Display(Name = "تصویر کالا")]
        public IFormFile Image { get; set; }
        [Display(Name = "تصویر اصلی")]
        public bool IsMain { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
