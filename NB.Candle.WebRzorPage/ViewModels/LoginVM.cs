using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Username { get; set; }
        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string Password { get; set; }
    }
}
