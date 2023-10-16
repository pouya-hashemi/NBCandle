using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class EmailConfiguration:Entity<Guid>
    {
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
