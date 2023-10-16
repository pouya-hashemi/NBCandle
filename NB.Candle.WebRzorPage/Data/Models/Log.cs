using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class Log:Entity<Guid>
    {
        public string Request { get; set; }
        public string Query { get; set; }
        public string Method { get; set; }
        public string Message { get; set; }
        public string InnerMessage { get; set; }
        public DateTime DateTime { get; set; }
    }
}
