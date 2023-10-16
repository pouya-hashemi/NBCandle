using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateNormalDiscountVM
    {
        public Guid ProductId { get; set; }
        [Display(Name ="درصد تخفیف")]
        public int DiscountPercentage { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }
        [Display(Name = "نمایش در خانه")]
        public bool DisplayOnHomePage { get; set; }
        [Display(Name = "عنوان تخفیف")]
        public string Title { get; set; }
        [Display(Name = "توضیحات تخفیف")]
        public string Description { get; set; }
    }
}
