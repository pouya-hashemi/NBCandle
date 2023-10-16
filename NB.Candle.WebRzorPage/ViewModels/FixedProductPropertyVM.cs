using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class FixedProductPropertyVM
    {
        public Guid ID { get; set; }
        public float? Height { get; set; }
        public float? Diameter { get; set; }
        public float? Width { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
