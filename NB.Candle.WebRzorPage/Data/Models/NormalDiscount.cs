using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class NormalDiscount : Entity<Guid>
    {
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int DiscountPercentage { get; set; }
        public string ImageUrl { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
