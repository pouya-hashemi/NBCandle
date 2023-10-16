using NB.Candle.WebRzorPage.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CreateFixedProductPropertyVM
    {
        public Guid ProductId { get; set; }
        public ProductPropertyVM ProductProperty { get; set; }
    }
}
