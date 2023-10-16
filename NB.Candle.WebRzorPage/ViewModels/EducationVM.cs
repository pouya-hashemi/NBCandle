using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class EducationVM
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public FileType FileType { get; set; }
        public string FileTypeStr { get; set; }
        public bool IsActive { get; set; }
    }
}
