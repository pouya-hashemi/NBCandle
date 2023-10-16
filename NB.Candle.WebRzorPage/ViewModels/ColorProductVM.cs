using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class ColorProductVM
    {
        public Guid ID { get; set; }
        public string DisplayName { get; set; }
        public string ColorCode { get; set; }
        public string ImageUrl { get; set; }
        public ColorType ColorType { get; set; }
        public string ColorTypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelected { get; set; }
    }
}
