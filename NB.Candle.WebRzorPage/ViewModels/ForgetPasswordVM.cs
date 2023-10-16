using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ForgetPasswordVM
    {
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string PhoneNumber { get; set; }
    }
}
