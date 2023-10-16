using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class NormalDiscountVM
    {
        public Guid ID { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsActive { get; set; }
    }
}
