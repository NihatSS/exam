using AutoMapper;
using Imtahan.Helper.Dto;
using Imtahan.Model;

namespace Imtahan.Helper.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductEditDto, Product>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });
        }
    }
}
