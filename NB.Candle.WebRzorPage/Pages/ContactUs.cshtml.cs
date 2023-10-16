using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;

namespace NB.Candle.WebRzorPage.Pages
{
    public class ContactUsModel : BasePage
    {
        public ContactUsModel(ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {

        }
        public void OnGet()
        {
        }
    }
}
