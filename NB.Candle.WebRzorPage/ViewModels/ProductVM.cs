using NB.Candle.WebRzorPage.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ProductVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public bool IsActive { get; set; }
        public bool IsFreeSize { get; set; }
        public NormalDiscountVM NormalDiscount { get; set; }
        public IEnumerable<ProductImageVM> ProductImages { get; set; }

    }
}
