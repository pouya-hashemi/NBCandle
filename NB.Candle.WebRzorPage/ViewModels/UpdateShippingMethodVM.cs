using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateShippingMethodVM
    {
        public Guid ID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Name { get; set; }
        [Display(Name = "هزینه")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public decimal Cost { get; set; }
        [Display(Name = "تعداد روز ارسال")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public int EstimatedDaysToDeliver { get; set; }
        [Display(Name = "نمایش در فاکتور")]
        public bool IsInInvoice { get; set; }
        [Display(Name = "روش پیش فرض")]
        public bool IsDefault { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
