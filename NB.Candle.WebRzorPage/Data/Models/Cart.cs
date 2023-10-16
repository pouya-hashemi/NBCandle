using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class Cart : Entity<Guid>
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public ProductProperty ProductProperty { get; set; }
        public int Quantity { get; set; }
        public Guid? ColorId{ get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }
    }
}
