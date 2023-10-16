using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Enums;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    [Authorize]
    public class CartModel : BasePage
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public CartModel(ApplicationDbContext applicationDbContext,
            IWebHostEnvironment webHostEnvironment,
            UserManager<User> userManager) : base(applicationDbContext)
        {
            this._webHostEnvironment = webHostEnvironment;
            this._userManager = userManager;
        }
        public IEnumerable<CartVM> CartItems { get; set; }
        public IEnumerable<CartShippingMethodVM> ShippingMethods { get; set; }
        public InvoiceVM Invoice { get; set; }
        public OrderDiscountVM OrderDiscount { get; set; }
        public Guid FirstCategoryId { get; set; }
        [BindProperty]
        public OrderSubmitVM OrderSubmit { get; set; }
        public async Task OnGet()
        {
            await PreparePage();

        }
        public async Task<IActionResult> OnPostDeleteCartItem(Guid Id)
        {
            var model = await _applicationDbContext.Carts
                .Where(w => w.ID == Id)
                .FirstOrDefaultAsync();

            _applicationDbContext.Carts.Remove(model);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("cart");
        }
        public async Task<IActionResult> OnPostChangeShippingMethod(Guid Id)
        {
            var userId = HttpContext.User.Claims
                            .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                            .Select(s => s.Value)
                            .FirstOrDefault();
            var model = await _applicationDbContext.CartInfos
                .Where(w => w.UserId == userId)
                .FirstOrDefaultAsync();

            model.ShippingMethodId = Id;

            _applicationDbContext.CartInfos.Update(model);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("cart");
        }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                await PreparePage();
                return Page();
            }

            var userId = HttpContext.User.Claims
                            .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                            .Select(s => s.Value)
                            .FirstOrDefault();


            var cart = await _applicationDbContext.Carts
                .Include(i => i.Product).ThenInclude(i => i.Category)
                .Include(i => i.Product).ThenInclude(i => i.Images)
                .Include(i => i.Product).ThenInclude(i => i.NormalDiscounts)
                .Include(i => i.Color)
                .Where(w => w.UserId == userId)
                .ToListAsync();

            var cartInfo = await _applicationDbContext.CartInfos
                .Include(i => i.ShippingMethod)
                .Where(w => w.UserId == userId)
                .FirstOrDefaultAsync();

            OrderDiscount = await _applicationDbContext.OrderDiscounts
               .Where(w => w.IsActive)
               .OrderByDescending(o => o.InsertDateTime)
               .Select(s=>new OrderDiscountVM() { 
               ID=s.ID,
               DiscountPersentage=s.DiscountPercentage,
               FloorAmount=s.FloorAmount,
               IsActive= s.IsActive
               })
               .FirstOrDefaultAsync();

            var lastOrderNumber = await _applicationDbContext.OrderMasters.OrderByDescending(o => o.OrderNumber).Select(m => m.OrderNumber).FirstOrDefaultAsync();
            lastOrderNumber = lastOrderNumber == 0 ? 1000 : lastOrderNumber + 1;
            var orderMaster = new OrderMaster()
            {
                ID = Guid.NewGuid(),
                ShippingMethodId = cartInfo.ShippingMethodId,
                UserId = userId,
                SubmitDate = DateTime.UtcNow,
                OrderNumber = lastOrderNumber,
                EstimatedDeliveryDate = DateTime.UtcNow.AddDays(cartInfo.ShippingMethod.EstimatedDaysToDeliver),
                ShippingAddreess = OrderSubmit.ShippingAddress,
                PostalCode = OrderSubmit.PostalCode,
                Description = OrderSubmit.Description,
                ShippingPhoneNumber = OrderSubmit.ShippingPhoneNumber,
                ShippingFullName = OrderSubmit.ShippingFullName,
                OrderStatus = OrderStatus.Submited,
                TotalProductAmount = cart.Sum(s => s.ProductProperty.Price * s.Quantity),
                ShippingCost = cartInfo.ShippingMethod.Cost,
                TotalDiscountAmount = cart.Sum(s => s.Product.NormalDiscounts.Where(w=>w.IsActive).OrderByDescending(o=>o.InsertDateTime).FirstOrDefault() != null ? s.ProductProperty.Price * ( s.Product.NormalDiscounts.Where(w => w.IsActive).OrderByDescending(o => o.InsertDateTime).Select(ss=>ss.DiscountPercentage).FirstOrDefault())*s.Quantity / 100 : 0),

            };
            if ((orderMaster.TotalProductAmount - orderMaster.TotalDiscountAmount) >= OrderDiscount.FloorAmount)
            {
                orderMaster.TotalDiscountAmount += (orderMaster.TotalProductAmount - orderMaster.TotalDiscountAmount) * (OrderDiscount.DiscountPersentage) / 100;
            }
            orderMaster.TaxAmount = (orderMaster.TotalProductAmount - orderMaster.TotalDiscountAmount) * 0.09m;
            orderMaster.TotalAmount = orderMaster.TotalProductAmount - orderMaster.TotalDiscountAmount + orderMaster.ShippingCost + orderMaster.TaxAmount;

            var file = Path.Combine(_webHostEnvironment.WebRootPath, "Images", "Payment", $"Payment_{orderMaster.ID}_{OrderSubmit.PaymentImage.FileName}");
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await OrderSubmit.PaymentImage.CopyToAsync(fileStream);
            }
            orderMaster.PaymentImageUrl = "/Images/Payment/" + $"Payment_{orderMaster.ID}_{OrderSubmit.PaymentImage.FileName}";

            orderMaster.OrderItems = cart.Select(s => new OrderItem()
            {
                ID = Guid.NewGuid(),
                ColorId = s.ColorId,
                ProductId = s.ProductId,
                ProductProperty = s.ProductProperty,
                Quantity = s.Quantity
            }).ToList();



            _applicationDbContext.OrderMasters.Add(orderMaster);

            _applicationDbContext.Carts.RemoveRange(cart);


            var products = await _applicationDbContext.Products
                .Where(w => (orderMaster.OrderItems.Select(s => s.ProductId).ToList().Contains(w.ID)))
                .ToListAsync();

            foreach (var product in products)
            {
                product.OrderCount += 1;
            }

            _applicationDbContext.Products.UpdateRange(products);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("MyOrders");
        }
        private async Task PreparePage()
        {

            var userId = HttpContext.User.Claims
                            .Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                            .Select(s => s.Value)
                            .FirstOrDefault();

            var user = await _userManager.FindByIdAsync(userId);

            if (OrderSubmit == null)
            {
                OrderSubmit = new OrderSubmitVM();
                OrderSubmit.ShippingAddress = user.UserAddress;
                OrderSubmit.PostalCode = user.PostalCode;
                OrderSubmit.ShippingFullName = user.FirstName + " " + user.LastName;
                OrderSubmit.ShippingPhoneNumber = user.PhoneNumber;
                
            }
            OrderDiscount = await _applicationDbContext.OrderDiscounts
                .Where(w => w.IsActive)
                .OrderByDescending(o => o.InsertDateTime)
                .Select(s => new OrderDiscountVM()
                {
                    ID = s.ID,
                    DiscountPersentage = s.DiscountPercentage,
                    FloorAmount = s.FloorAmount,
                    IsActive = s.IsActive
                })
                .FirstOrDefaultAsync();

            CartItems = await _applicationDbContext.Carts
                .Include(i => i.Product).ThenInclude(i => i.Category)
                .Include(i => i.Product).ThenInclude(i => i.Images)
                .Include(i => i.Product).ThenInclude(i => i.NormalDiscounts)
                .Include(i => i.Color)
                .Where(w => w.UserId == userId)
                .Select(s => new CartVM()
                {
                    ID = s.ID,
                    Quantity = s.Quantity,
                    Product = new ProductVM()
                    {
                        ID = s.Product.ID,
                        CategoryName = s.Product.Category.Name,
                        Name = s.Product.Name,
                        ProductImages = s.Product.Images.Where(w => w.IsMain).Select(ss => new ProductImageVM()
                        {
                            ID = ss.ID,
                            Url = ss.Url
                        }).ToList(),
                        NormalDiscount = s.Product.NormalDiscounts.Where(w => w.IsActive)
                                        .OrderByDescending(o => o.InsertDateTime)
                                        .Select(d => new NormalDiscountVM()
                                        {
                                            ID = d.ID,
                                            DiscountPercentage = d.DiscountPercentage
                                        })
                                        .FirstOrDefault()
                    },
                    ProductProperty = new ProductPropertyVM()
                    {
                        Height = s.ProductProperty.Height,
                        Width = s.ProductProperty.Width,
                        Diameter = s.ProductProperty.Diameter,
                        Price = s.ProductProperty.Price
                    },
                    Color = s.ColorId != null ? new ColorVM()
                    {
                        ID = s.Color.ID,
                        ColorCode = s.Color.ColorCode,
                        DisplayName = s.Color.DisplayName,
                        ColorType = s.Color.ColorType,
                        ImageUrl = s.Color.ImageUrl
                    } : null
                })
                .ToListAsync();

            FirstCategoryId = await _applicationDbContext.Categories
               .OrderBy(w => w.Sequence)
               .Select(s => s.ID)
               .FirstOrDefaultAsync();
            var cartInfo = await _applicationDbContext.CartInfos.Where(a => a.UserId == userId).FirstOrDefaultAsync();

            ShippingMethods = await _applicationDbContext.ShippingMethods
                .Where(w => w.IsActive)
                .Select(s => new CartShippingMethodVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    Cost = s.Cost,
                    IsSelected = cartInfo.ShippingMethodId == s.ID,
                    IsInInvoice = s.IsInInvoice
                })
                .ToListAsync();

            Invoice = new InvoiceVM()
            {
                ProductSum = CartItems.Sum(s => s.ProductProperty.Price *s.Quantity),
                DiscountSum = CartItems.Sum(s=>s.Product.NormalDiscount!=null?s.ProductProperty.Price*(s.Product.NormalDiscount.DiscountPercentage) * s.Quantity / 100:0),
                ShippingCost = ShippingMethods.Where(w => w.IsSelected && w.IsInInvoice).Select(s => s.Cost).FirstOrDefault()
            };
            if (OrderDiscount!=null &&(Invoice.ProductSum - Invoice.DiscountSum) >= OrderDiscount.FloorAmount)
            {
                Invoice.OrderDiscountSum = (Invoice.ProductSum - Invoice.DiscountSum) * (OrderDiscount.DiscountPersentage) / 100;
            }
            Invoice.TaxAmount = (Invoice.ProductSum - Invoice.DiscountSum- Invoice.OrderDiscountSum) * 0.09m;
            Invoice.TotalCost = Invoice.ProductSum - Invoice.DiscountSum - Invoice.OrderDiscountSum + Invoice.ShippingCost + Invoice.TaxAmount;
            
        }
    }
}
