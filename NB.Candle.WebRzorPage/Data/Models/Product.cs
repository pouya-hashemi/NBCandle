using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class Product:Entity<Guid>
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsFreeSize { get; set; }
        public long OrderCount { get; set; }
        public ProductType ProductType{ get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public IEnumerable<ProductImage> Images { get; set; }
        public IEnumerable<FixedProductProperty> FixedProductProperties { get; set; }
        public IEnumerable<Color> Colors { get; set; }
        public IEnumerable<NormalDiscount> NormalDiscounts { get; set; }
    }
}
