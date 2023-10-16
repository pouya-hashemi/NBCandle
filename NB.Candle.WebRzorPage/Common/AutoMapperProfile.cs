using AutoMapper;
using NB.Candle.WebRzorPage.Data.Models;
using NB.Candle.WebRzorPage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NB.Candle.WebRzorPage.Common
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserVM, User>().IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForMember(f=>f.Email,e=>e.MapFrom(m=>m.Username));

            CreateMap<CreateCategoryVM, Category>();
            CreateMap<Category, CategoryVM>()
                .ForMember(f => f.ParentName, e => e.MapFrom(m => m.ParentCategory.Name)); 
            CreateMap<Category, UpdateCategoryVM>();


            CreateMap<CreateProductVM,Product>();
            CreateMap<Product, UpdateProductVM>();

            CreateMap<CreateFixedProductPropertyVM,FixedProductProperty>();
            CreateMap<ProductPropertyVM, ProductProperty>();
            CreateMap<ProductProperty,ProductPropertyVM>();
            CreateMap<FixedProductProperty, UpdateFixedProductPropertyVM>();

            CreateMap<ProductImage, UpdateProductImageVM>();
            CreateMap<ProductImage, ProductImageVM>();
            CreateMap<CreateProductImageVM, ProductImage>();

            CreateMap<CreateShippingMethodVM, ShippingMethod>();
            CreateMap<ShippingMethod, UpdateShippingMethodVM>();

            CreateMap<CreateColorVM, Color>();
            CreateMap<Color,ColorVM>();

            CreateMap<CreateNormalDiscountVM, NormalDiscount>();
            CreateMap<NormalDiscount, UpdateNormalDiscountVM>();
            
            CreateMap<CreateOrderDiscountVM, OrderDiscount>();
            CreateMap<OrderDiscount, UpdateOrderDiscountVM>();
            
            CreateMap<CreateEducationVM, Education>();
            CreateMap<Education, UpdateEducationVM>();
                
        }
    }
}
