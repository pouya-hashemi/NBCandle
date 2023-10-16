using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class OrderItemVM
    {
        public ProductDisplayVM Product { get; set; }
        public ProductPropertyVM ProductProperty { get; set; }
        public ColorVM Color { get; set; }
        public int Quantity { get; set; }
    }
}
