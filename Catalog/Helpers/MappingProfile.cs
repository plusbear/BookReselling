using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Models;
using System.Linq;

namespace Catalog.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForCreation, Product>().ForMember(p => p.Images, opt => opt.Ignore());
            CreateMap<Product, ProductDto>().ForMember(p => p.ImageRefs, opt => opt.MapFrom(p => p.Images.Select(i => i.ImageRef)));
        }
    }
}
