using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages
{
    [Authorize(Policy ="Vendor")]
    public class indexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
