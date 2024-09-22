using AutoMapper;
using ShoppingApp.API.Dtos;
using ShoppingApp.API.Models;

namespace ShoppingApp.API.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
