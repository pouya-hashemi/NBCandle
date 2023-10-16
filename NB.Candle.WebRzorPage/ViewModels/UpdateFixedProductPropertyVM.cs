using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class UpdateFixedProductPropertyVM
    {
        public Guid ID { get; set; }
        public Guid ProductId { get; set; }
        public ProductPropertyVM ProductProperty { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
