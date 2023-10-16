using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ProductDisplayVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public ProductType ProductType { get; set; }
        public string CategoryName { get; set; }
        public bool IsFreeSize { get; set; }
        public NormalDiscountVM NormalDiscount { get; set; }
        public IEnumerable<FixedProductPropertyVM> ProductProperties { get; set; }
        public IEnumerable<ProductImageVM> ProductImages { get; set; }
        public IEnumerable<ColorVM> Colors { get; set; }
    }
}
