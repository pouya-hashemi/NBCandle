using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateCategoryVM
    {
        public Guid ID { get; set; }
        [Display(Name = "نام دسته بندی")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile Image { get; set; }
        [Display(Name = "اولویت")]
        public int Sequence { get; set; }
        [Display(Name = "نام دسته بندی پدر")]
        public Guid? ParentCategoryId { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
