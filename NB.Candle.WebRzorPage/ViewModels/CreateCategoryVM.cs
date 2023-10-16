using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateCategoryVM
    {
        [Display(Name ="نام دسته بندی")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public IFormFile Image { get; set; }
        [Display(Name = "اولویت")]
        public int Sequence { get; set; }
        [Display(Name ="نام دسته بندی پدر")]
        public Guid? ParentCategoryId { get; set; }
    }
}
