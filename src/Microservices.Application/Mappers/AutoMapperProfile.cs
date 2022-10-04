using AutoMapper;
using Microservices.Domain.Entities;
using Microservices.Grpc;

namespace Microservices.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductEntity, Product>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Id))
                .ReverseMap();
        }
    }
}