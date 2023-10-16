using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class OrderSubmitVM
    {
        [Display(Name = "تصویر پرداختی")]
        [Required(ErrorMessage = "افزودن {0} الزامیست")]
        public IFormFile PaymentImage{ get; set; }
        [Display(Name ="آدرس تحویل گیرنده")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string ShippingAddress { get; set; }
        [Display(Name ="نام تحویل گیرنده")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string ShippingFullName { get; set; }
        [Display(Name ="شماره تماس تحویل گیرنده")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string ShippingPhoneNumber { get; set; }
        [Display(Name ="کد پستی تحویل گیرنده")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string PostalCode { get; set; }
        [Display(Name ="توضیحات سفارش")]
        public string Description { get; set; }
    }
}
