using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class CartShippingMethodVM
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public bool IsInInvoice { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDefault { get; set; }
        public int EstimatedDaysToDeliver { get; set; }
    }
}
