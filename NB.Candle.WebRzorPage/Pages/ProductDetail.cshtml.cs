using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NB.Candle.WebRzorPage.Common;
using NB.Candle.WebRzorPage.Data;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;

namespace NB.Candle.WebRzorPage.Pages
{
    public class ProductDetailModel : BasePage
    {
        private readonly IMapper _mapper;

        public ProductDetailModel(ApplicationDbContext applicationDbContext,
            IMapper mapper) : base(applicationDbContext)
        {
            this._mapper = mapper;
        }
        public ProductDisplayVM Product { get; set; }
        [BindProperty]
        public AddToCartVM AddToCart { get; set; }
        public async Task OnGet(Guid productId)
        {
            AddToCart = new AddToCartVM() { ProductId = productId };
            await PreparePage(productId);
        }

        public async Task<IActionResult> OnPost()
        {
            await PreparePage(AddToCart.ProductId);
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToPage("login", new { returnUrl = $"/productDetail?productId={AddToCart.ProductId}" });
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (!AddToCart.IsCustomeSize && AddToCart.FixedProductPropertyId == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا یکی از اقلام را انتخاب نمایید");
                return Page();
            }
            if (Product.ProductType==Data.Enums.ProductType.candle&& AddToCart.ColorId == null)
            {
                ModelState.AddModelError(string.Empty, "لطفا یکی از رنگ ها را انتخاب نمایید");
                return Page();
            }
            if (AddToCart.IsCustomeSize &&
                (AddToCart.Height == null /*|| AddToCart.Width==null||AddToCart.Diameter==null*/))
            {
                ModelState.AddModelError(string.Empty, "لطفا اطلاعات آیتم مورد نظر خود را تکمیل فرمایید");
                return Page();
            }
            var productProperty = new Data.Models.ProductProperty();
            if (AddToCart.FixedProductPropertyId != null)
            {
                productProperty = await _applicationDbContext.FixedProductProperties
                    .Include(i => i.ProductProperty)
                    .AsNoTracking()
                    .Where(w => w.ID == AddToCart.FixedProductPropertyId)
                    .Select(s => s.ProductProperty)
                    .FirstOrDefaultAsync();

            }
            else
            {
                productProperty = new Data.Models.ProductProperty()
                {
                    Height = AddToCart.Height,
                    Width = AddToCart.Width,
                    Diameter = AddToCart.Diameter,
                };
            }
            var userId = HttpContext.User.Claims.Where(w => w.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")
                .Select(s => s.Value)
                .FirstOrDefault();

            var cart = new Cart();

            cart = await _applicationDbContext.Carts
                .Include(i=>i.ProductProperty)
                .Where(w => w.UserId == userId)
                .Where(w=>w.ProductId == AddToCart.ProductId)
                .Where(w=>w.ProductProperty.Diameter==productProperty.Diameter)
                .Where(w=>w.ProductProperty.Height==productProperty.Height)
                .Where(w=>w.ProductProperty.Width==productProperty.Width)
                .Where(w=>w.ColorId==AddToCart.ColorId)
               .FirstOrDefaultAsync();
            if (cart != null)
            {
                cart.Quantity = AddToCart.Quantity;
                _applicationDbContext.Carts.Update(cart);
            }
            else
            {
                cart = new Cart()
                {
                    ID = Guid.NewGuid(),
                    ProductId = AddToCart.ProductId,
                    ProductProperty = productProperty,
                    Quantity = AddToCart.Quantity,
                    UserId = userId,
                    ColorId=AddToCart.ColorId
                };
            _applicationDbContext.Carts.Add(cart);
            }
           
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToPage("/ProductDetail",new { productId= AddToCart.ProductId });
        }
        private async Task PreparePage(Guid productId)
        {
            Product = await _applicationDbContext.Products
                .Include(i => i.Category)
                .Include(i => i.NormalDiscounts)
                .Include(i => i.FixedProductProperties).ThenInclude(i => i.ProductProperty)
                .Include(i => i.Images)
                .Where(w => w.ID == productId)
                .Select(s => new ProductDisplayVM()
                {
                    ID = s.ID,
                    Name = s.Name,
                    CategoryName = s.Category.Name,
                    IsFreeSize = s.IsFreeSize,
                    ProductType=s.ProductType,
                    ProductImages = s.Images
                               .Where(w => w.IsActive)
                               .Select(m => new ProductImageVM()
                               {
                                   ID = m.ID,
                                   Url = m.Url,
                                   IsMain = m.IsMain
                               }).AsEnumerable(),

                    ProductProperties = s.FixedProductProperties
                                   .Where(w => w.IsActive)
                                   .Select(p => new FixedProductPropertyVM()
                                   {
                                       ID = p.ID,
                                       Height = p.ProductProperty.Height,
                                       Price = p.ProductProperty.Price,
                                       Width = p.ProductProperty.Width,
                                       Diameter = p.ProductProperty.Diameter,
                                   }).AsEnumerable(),
                    Colors = s.Colors.Select(c => new ColorVM()
                    {
                        ID = c.ID,
                        ColorCode = c.ColorCode,
                        DisplayName = c.DisplayName,
                        ColorType=c.ColorType,
                        ImageUrl=c.ImageUrl
                    }),
                    NormalDiscount = s.NormalDiscounts.Where(w => w.IsActive)
                    .OrderByDescending(o => o.InsertDateTime)
                    .Select(d => new NormalDiscountVM()
                    {
                        ID = d.ID,
                        DiscountPercentage = d.DiscountPercentage
                    })
                    .FirstOrDefault()
                })
                .FirstOrDefaultAsync();

        }
    }
}
