using AutoMapper;
using Avonale.Products.Domain.Entities;
using Avonale.Products.Shared.Core.DTO;

namespace Avonale.Products.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Product, ProductDetailedDTO>();
        CreateMap<Product, ProductDTO>();

    }
}