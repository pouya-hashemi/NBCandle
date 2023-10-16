using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ProductPropertyVM
    {
        [Display(Name = "طول")]
        public float? Height { get; set; }
        [Display(Name = "عرض")]
        public float? Diameter { get; set; }
        [Display(Name = "قطر")]
        public float? Width { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public decimal Price { get; set; }
    }
}
