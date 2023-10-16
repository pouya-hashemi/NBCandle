using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateProductVM
    {
        public Guid ID { get; set; }
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Name { get; set; }
        [Display(Name = "نوع کالا")]
        public ProductType ProductType { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public Guid CategoryId { get; set; } 
        [Display(Name = "وضعیت")]
        public bool IsActive{ get; set; }
        [Display(Name = "سایز دلخواه")]
        public bool IsFreeSize { get; set; }
    }
}
