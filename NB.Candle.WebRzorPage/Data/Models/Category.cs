using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class Category:Entity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Sequence { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentCategoryId { get; set; }
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }
        public ICollection<Category> Childrens { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
