using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class ShippingMethod : Entity<Guid>
    {
        public string Name { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Cost { get; set; }
        public int EstimatedDaysToDeliver { get; set; }
        public bool IsInInvoice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
    }
}
