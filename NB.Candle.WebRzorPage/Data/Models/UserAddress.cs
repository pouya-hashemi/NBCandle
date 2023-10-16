using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class UserAddress:Entity<Guid>
    {
        public string Street { get; set; }
        public string Detail { get; set; }
        public string PostalCode { get; set; }
        //public string UserId { get; set; }
        //[ForeignKey("UserId")]
        //public User User { get; set; }
    }
}
