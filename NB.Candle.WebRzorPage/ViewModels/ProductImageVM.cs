using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ProductImageVM
    {
        public Guid ID { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
        public bool IsActive { get; set; }
    }
}
