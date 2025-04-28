using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;

namespace Ecom.API.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.category.name))
                .ReverseMap();

            CreateMap<Photo, PhotoDTO>().ReverseMap();
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.photos, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<UpdateProductDTO, Product>()
               .ForMember(dest => dest.photos, opt => opt.Ignore())
               .ReverseMap();

        }
    }
}
