using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Data.Models
{
    public class OrderMaster : Entity<Guid>
    {
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid ShippingMethodId { get; set; }
        [ForeignKey("ShippingMethodId")]
        public ShippingMethod ShippingMethod { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public long OrderNumber { get; set; }
        public string PostTrackingNumber { get; set; }
        public string PaymentImageUrl { get; set; }
        public string ShippingAddreess { get; set; }
        public string ShippingFullName { get; set; }
        public string ShippingPhoneNumber { get; set; }
        public string TraceNumber { get; set; }
        public string Description { get; set; }
        public string PostalCode { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal TotalProductAmount { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal TotalDiscountAmount { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal ShippingCost { get; set; }
        [Column(TypeName = "decimal(18,0)")]
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus{ get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

    }
}
