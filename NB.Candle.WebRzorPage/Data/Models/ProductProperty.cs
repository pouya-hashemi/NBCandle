using NB.Candle.WebRzorPage.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class ProductProperty:ValueObject
    {
        public float? Height { get; set; }
        public float? Diameter { get; set; }
        public float? Width { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal Price { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Height;
            yield return this.Diameter;
            yield return this.Width;
            yield return this.Price;
        }
    }
}
