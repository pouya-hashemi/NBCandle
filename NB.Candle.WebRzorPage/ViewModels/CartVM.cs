using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CartVM
    {
        public Guid ID { get; set; }
        public ProductVM Product { get; set; }
        public ProductPropertyVM ProductProperty { get; set; }
        public int Quantity { get; set; }
        public ColorVM Color { get; set; }
    }
}
