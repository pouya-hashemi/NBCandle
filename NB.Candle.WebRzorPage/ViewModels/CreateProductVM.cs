using Microsoft.AspNetCore.Http;
using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateProductVM
    {
        [Display(Name = "نام کالا")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Name { get; set; }
        [Display(Name = "نوع کالا")]
        public ProductType ProductType { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public Guid CategoryId { get; set; }
        [Display(Name = "سایز دلخواه")]
        public bool IsFreeSize { get; set; }
        //[Display(Name = "طول")]
        //public float? Height { get; set; }
        //[Display(Name = "عرض")]
        //public float? Width { get; set; }
        //[Display(Name = "قطر")]
        //public float? Diameter { get; set; }
        //[Display(Name = "قیمت")]
        //[Required(ErrorMessage = "{0} الزامیست")]
        //public decimal Price { get; set; }
        //[Display(Name = "تصویر")]
        //[Required(ErrorMessage = "{0} الزامیست")]
        //public IFormFile Image { get; set; }

    }
}
