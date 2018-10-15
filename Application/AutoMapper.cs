using Application.Models;
using AutoMapper;
using Domain.Entities.EntityAggregate;
using Domain.Entities;

namespace Application
{
    public static class AutoMapper
    {
        public static void MapperRegister()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataType, DataTypeDomain>(MemberList.Source).ReverseMap();

                cfg.CreateMap<Entity, EntityDomain>(MemberList.Source)
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.Attributes, opt => opt.Ignore())
                    .ReverseMap();

                cfg.CreateMap<Attribute, AttributeDomain>(MemberList.Source)
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.Entity, opt => opt.Ignore())
                    .ForMember(domain => domain.DataType, opt => opt.Ignore())
                    .ReverseMap();
            });
        }
    }
}
