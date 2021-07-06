using AutoMapper;
using Catalog.DataTransferObjects;
using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
