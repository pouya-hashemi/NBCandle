using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class FixedProductProperty:Entity<Guid>
    {
        public ProductProperty ProductProperty { get; set; }
        public bool IsActive { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
