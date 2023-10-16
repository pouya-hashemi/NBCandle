using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Enums;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Areas.Vendor.Pages.Order
{
    public class DetailModel : PageModel
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DetailModel(ApplicationDbContext applicationDbContext,
            IWebHostEnvironment webHostEnvironment)
        {
            this._applicationDbContext = applicationDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        public OrderMasterVM OrderMaster { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "کد پیگیری الزامیست")]
        [Display(Name = "کد پیگیری")]
        public string TraceNumber { get; set; }
        public async Task OnGet(Guid Id)
        {
            await PreparePage(Id);
        }
        public async Task<IActionResult> OnPostConfirmOrder(Guid Id)
        {
            var orderMasterModel = await _applicationDbContext.OrderMasters
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();
            orderMasterModel.OrderStatus = Data.Enums.OrderStatus.Confirmed;
            _applicationDbContext.OrderMasters.Update(orderMasterModel);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("Detail", new { Id = Id });
        }
        public async Task<IActionResult> OnPostShipOrder(Guid Id)
        {
            if (!ModelState.IsValid)
            {
                await PreparePage(Id);
                return Page();
            }
            var orderMasterModel = await _applicationDbContext.OrderMasters
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();
            orderMasterModel.TraceNumber = TraceNumber;
            orderMasterModel.OrderStatus = Data.Enums.OrderStatus.Shipped;
            _applicationDbContext.OrderMasters.Update(orderMasterModel);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("Detail", new { Id = Id });
        }
        public async Task<IActionResult> OnPostDeliverOrder(Guid Id)
        {
            var orderMasterModel = await _applicationDbContext.OrderMasters
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();
            orderMasterModel.OrderStatus = Data.Enums.OrderStatus.Delivered;
            _applicationDbContext.OrderMasters.Update(orderMasterModel);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("Detail", new { Id = Id });
        }
        public async Task<IActionResult> OnPostGoBackState(Guid Id)
        {
            var orderMasterModel = await _applicationDbContext.OrderMasters
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();
            switch (orderMasterModel.OrderStatus)
            {
                case Data.Enums.OrderStatus.Submited:
                    break;
                case Data.Enums.OrderStatus.Confirmed:
                    {
                        orderMasterModel.OrderStatus = OrderStatus.Submited;
                        break;
                    }
                case Data.Enums.OrderStatus.Shipped:
                    {
                        orderMasterModel.OrderStatus = OrderStatus.Confirmed;
                        break;
                    }
                case Data.Enums.OrderStatus.Delivered:
                    {
                        orderMasterModel.OrderStatus = OrderStatus.Shipped;
                        orderMasterModel.TraceNumber = "";
                        break;
                    }
                default:
                    break;
            }
            _applicationDbContext.OrderMasters.Update(orderMasterModel);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("Detail", new { Id = Id });
        }
        public async Task<IActionResult> OnGetPaymentImage(Guid Id)
        {
            var orderMasterModel = await _applicationDbContext.OrderMasters
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();
            var path = _webHostEnvironment.ContentRootPath + "\\wwwroot" + orderMasterModel.PaymentImageUrl.Replace('/', '\\');
            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);
            //Send the File to Download.
            return File(bytes, "application/octet-stream", $"Payment-{orderMasterModel.OrderNumber}.png");
        }
        private async Task PreparePage(Guid Id)
        {
            OrderMaster = await _applicationDbContext.OrderMasters
                .Include(i => i.User)
                .Include(i => i.ShippingMethod)
                .Include(i => i.OrderItems).ThenInclude(i => i.Product).ThenInclude(t => t.Images)
                .Include(i => i.OrderItems).ThenInclude(t => t.Color)
                .Include(i => i.OrderItems).ThenInclude(t => t.ProductProperty)
                .Where(w => w.ID == Id)
                .Select(s => new OrderMasterVM()
                {
                    ID = s.ID,
                    EstimatedDeliveryDate = s.EstimatedDeliveryDate,
                    OrderNumber = s.OrderNumber,
                    OrderStatus = s.OrderStatus.GetDescription(),
                    OrderStatusNumber = s.OrderStatus,
                    PostalCode = s.PostalCode,
                    PostTrackingNumber = s.PostTrackingNumber,
                    ShippingAddreess = s.ShippingAddreess,
                    ShippingFullName = s.ShippingFullName,
                    ShippingPhoneNumber = s.ShippingPhoneNumber,
                    SubmitDate = s.SubmitDate,
                    TotalAmount = s.TotalAmount,
                    Description=s.Description,
                    TotalDiscountAmount = s.TotalDiscountAmount,
                    TaxAmount = s.TaxAmount,
                    ShippingCost = s.ShippingCost,
                    TotalProductAmount = s.TotalProductAmount,
                    UserFullName = s.User.FirstName + " " + s.User.LastName,
                    Username = s.User.UserName,
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
                .FirstOrDefaultAsync();
        }
    }
}
