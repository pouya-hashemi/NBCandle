using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class Color:Entity<Guid>
    {
        public string DisplayName { get; set; }
        public string ColorCode { get; set; }
        public string ImageUrl { get; set; }
        public ColorType ColorType { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}
