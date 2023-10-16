using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Pages
{
    public class PrivacyModel : BasePage
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger,
           ApplicationDbContext applicationDbContext):base(applicationDbContext)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
