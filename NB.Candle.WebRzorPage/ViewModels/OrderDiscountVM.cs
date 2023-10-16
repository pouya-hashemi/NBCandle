using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class OrderDiscountVM
    {
        public Guid ID { get; set; }
        public decimal FloorAmount { get; set; }
        public int DiscountPersentage { get; set; }
        public bool IsActive { get; set; }
    }
}
