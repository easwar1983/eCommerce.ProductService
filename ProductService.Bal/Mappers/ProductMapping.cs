using AutoMapper;
using ProductService.Bal.DTO;
using ProductService.Dal.Entities;

namespace ProductService.Bal.Mappers;
public class ProductMapping:Profile
{
    // Add your mapping configurations here
    // For example:
    // CreateMap<SourceEntity, DestinationDto>();
    // CreateMap<DestinationDto, SourceEntity>();
    public ProductMapping()
    {
        #region "Products(Entity) To ProductResponse(DTO)"
        CreateMap<Products, ProductResponse>()
             .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
             .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
             .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
             .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
        ;
        #endregion

        #region "ProductAddRequest(DTO) To Products(Entity)"
        CreateMap<ProductAddRequest, Products>()
            .ForMember(dest => dest.ProductId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
        ;
        #endregion

        #region "ProductUpdateRequest(DTO) To Products(Entity)"
        CreateMap<ProductUpdateRequest, Products>()
           .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
           .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
           .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
           .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
           .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock))
        ;
        #endregion
    }
}
