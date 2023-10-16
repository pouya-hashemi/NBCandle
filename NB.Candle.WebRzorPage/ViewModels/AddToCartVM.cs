using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class AddToCartVM
    {
        public Guid ProductId { get; set; }
        public Guid? FixedProductPropertyId{ get; set; }
        [Display(Name = "سفارش با سایز دلخواه")]
        public bool IsCustomeSize { get; set; }
        [Display(Name = "طول")]
        public float? Height { get; set; }
        [Display(Name = "عرض")]
        public float? Diameter { get; set; }
        [Display(Name = "قطر")]
        public float? Width { get; set; }
        [Display(Name ="تعداد")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public int Quantity { get; set; }
        [Display(Name = "رنگ")]
        public Guid? ColorId { get; set; }
    }
}
