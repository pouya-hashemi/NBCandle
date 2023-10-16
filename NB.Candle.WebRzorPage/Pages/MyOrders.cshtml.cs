using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class MyOrdersModel : BasePage
    {
        public IEnumerable<OrderMasterVM> OrderMasters { get; set; }
        public Guid FirstCategoryId { get; set; }
        public MyOrdersModel(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        public async Task OnGet()
        {
            FirstCategoryId = await _applicationDbContext.Categories
              .OrderBy(w => w.Sequence)
              .Select(s => s.ID)
              .FirstOrDefaultAsync();

            var userId = HttpContext.User.Claims
                           .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                           .Select(s => s.Value)
                           .FirstOrDefault();

            OrderMasters = await _applicationDbContext.OrderMasters
                .Include(i => i.ShippingMethod)
                .Include(i => i.OrderItems).ThenInclude(i => i.Product).ThenInclude(t => t.Images)
                .Include(i => i.OrderItems).ThenInclude(t => t.Color)
                .Include(i => i.OrderItems).ThenInclude(t => t.ProductProperty)
                .Where(w => w.UserId == userId)
                .OrderByDescending(o=>o.SubmitDate)
                .Select(s => new OrderMasterVM()
                {
                    ID = s.ID,
                    EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                    OrderNumber = s.OrderNumber,
                    OrderStatus = s.OrderStatus.GetDescription(),
                    PostalCode = s.PostalCode,
                    PostTrackingNumber = s.PostTrackingNumber,
                    ShippingAddreess = s.ShippingAddreess,
                    ShippingFullName = s.ShippingFullName,
                    ShippingPhoneNumber = s.ShippingPhoneNumber,
                    SubmitDate = s.SubmitDate,
                    TotalAmount = s.TotalAmount,
                    TotalDiscountAmount = s.TotalDiscountAmount,
                    TaxAmount = s.TaxAmount,
                    ShippingCost = s.ShippingCost,
                    TotalProductAmount = s.TotalProductAmount,
                    TraceNumber=s.TraceNumber,
                    ShippingMethod = new CartShippingMethodVM()
                    {
                        ID = s.ShippingMethod.ID,
                        Cost = s.ShippingMethod.Cost,
                        Name = s.ShippingMethod.Name,
                    },
                    OrderItems = s.OrderItems.Select(i => new OrderItemVM()
                    {
                        Product = new ProductDisplayVM()
                        {

                            ID = i.Product.ID,
                            Name = i.Product.Name,
                            ProductImages = new List<ProductImageVM>() { new ProductImageVM() { Url = i.Product.Images.Where(w => w.IsMain).Select(s => s.Url).FirstOrDefault() } },
                        },
                        Color = i.ColorId != null ? new ColorVM()
                        {
                            ColorCode = i.Color.ColorCode,
                            ColorType = i.Color.ColorType,
                            ImageUrl = i.Color.ImageUrl
                        } : null,
                        ProductProperty = new ProductPropertyVM()
                        {
                            Height = i.ProductProperty.Height,
                            Width = i.ProductProperty.Width,
                            Diameter = i.ProductProperty.Diameter,
                            Price = i.ProductProperty.Price,
                        },
                        Quantity = i.Quantity

                    })
                })
                .ToListAsync();
        }
    }
}
