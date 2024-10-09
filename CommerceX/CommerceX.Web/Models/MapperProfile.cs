using AutoMapper;
using CommerceX.Data.Concrete;

namespace CommerceX.Web.Models;

public class MapperProfile : Profile

{
    public MapperProfile()
    {
        CreateMap<Product, ProductViewModel>();
    }
}