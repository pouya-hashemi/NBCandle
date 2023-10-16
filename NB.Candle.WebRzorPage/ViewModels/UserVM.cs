using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UserVM
    {
        [Display(Name ="شماره تماس")]
        [Required(ErrorMessage ="{0} الزامیست")]
        public string Username { get; set; }
        [Display(Name ="رمز عبور")]
        [Required(ErrorMessage = "{0} الزامیست")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از {1} کارکتر باشد")]
        public string Password { get; set; }
        [Display(Name ="تکرار رمز عبور")]
        [Required(ErrorMessage = "{0} الزامیست")]
        [MinLength(6, ErrorMessage = "{0} باید بیشتر از {1} کارکتر باشد")]
        public string PasswordReEnter { get; set; }
        //[Display(Name ="شماره تماس")]
        //[Required(ErrorMessage = "{0} الزامیست")]
        //public string PhoneNumber { get; set; }
        [Display(Name ="نام")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string FirstName { get; set; }
        [Display(Name ="نام خانوادگی")]
        [Required(ErrorMessage = "{0} الزامیست")]
        public string LastName { get; set; }
    }
}
