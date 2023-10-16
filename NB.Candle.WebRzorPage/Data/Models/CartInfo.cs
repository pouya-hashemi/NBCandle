using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class CartInfo:Entity<Guid>
    {
        public Guid ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod ShippingMethod { get; set; }
        public string UserId { get; set; }
    }
}
