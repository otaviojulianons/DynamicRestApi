using Application.Models;
using AutoMapper;
using Domain.Entities.EntityAggregate;
using Domain.Entities;
using Domain.Core.ValueObjects;

namespace Application
{
    public static class AutoMapper
    {
        public static void MapperRegister()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataType, DataTypeDomain>(MemberList.Source).ReverseMap();

                cfg.CreateMap<AttributeDomain, Attribute>(MemberList.Source)
                    .ForMember(model => model.DataType, opt => opt.MapFrom( x => x.DataType.Name))
                    .ForMember(model => model.Name, opt => opt.MapFrom(x => x.Name ))
                    .ReverseMap();

                cfg.CreateMap<Entity, EntityDomain>(MemberList.Source)
                    .ForMember(domain => domain.Name, opt => opt.MapFrom( x => new Name(x.Name)))
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.Attributes, opt => opt.Ignore())
                    .ReverseMap();
            });
        }
    }
}
