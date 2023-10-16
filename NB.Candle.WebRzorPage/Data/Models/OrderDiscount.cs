using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class OrderDiscount:Entity<Guid>
    {
        [Column(TypeName ="decimal(18,0)")]
        public decimal FloorAmount { get; set; }
        public int DiscountPercentage { get; set; }
        public string ImageUrl { get; set; }
        public bool DisplayOnHomePage { get; set; }
        public bool IsActive { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
