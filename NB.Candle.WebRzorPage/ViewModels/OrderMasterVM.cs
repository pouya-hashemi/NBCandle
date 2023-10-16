using NB.Candle.WebRzorPage.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.ViewModels
{
    public class OrderMasterVM
    {
        public Guid ID { get; set; }
        public CartShippingMethodVM ShippingMethod { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public long OrderNumber { get; set; }
        public string PostTrackingNumber { get; set; }
        public string ShippingAddreess { get; set; }
        public string ShippingFullName { get; set; }
        public string ShippingPhoneNumber { get; set; }
        public string PostalCode { get; set; }
        public decimal TotalProductAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public decimal TotalAmount { get; set; }
        public string TraceNumber { get; set; }
        public string OrderStatus { get; set; }
        public string Description { get; set; }
        public OrderStatus OrderStatusNumber { get; set; }
        public string UserFullName { get; set; }
        public string Username { get; set; }
        public IEnumerable<OrderItemVM> OrderItems { get; set; }
    }
}
