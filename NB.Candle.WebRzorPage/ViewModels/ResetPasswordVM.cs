using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ResetPasswordVM
    {
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} الزامیست")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "{0} الزامیست")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از {1} کارکتر باشد")]
        [Compare("Password", ErrorMessage = "{0} با رمز عبور همخوانی ندارد")]
        public string PasswordReEnter { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
    }
}
