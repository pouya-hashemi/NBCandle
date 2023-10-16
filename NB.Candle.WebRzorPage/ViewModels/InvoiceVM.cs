using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class InvoiceVM
    {
        public decimal ProductSum { get; set; }
        public decimal DiscountSum { get; set; }
        public decimal OrderDiscountSum { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalCost { get; set; }
        public decimal FinalCost { get; set; }
    }
}
