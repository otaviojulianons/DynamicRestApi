using Application.Models;
using AutoMapper;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;

namespace Api
{
    public class AutoMapper
    {
        public static void MapperRegister()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DataType, DataTypeDomain>(MemberList.Source).ReverseMap();

                cfg.CreateMap<Entity, EntityDomain>(MemberList.Source)
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ReverseMap();

                cfg.CreateMap<Attribute, AttributeDomain>(MemberList.Source)
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.DataTypeId, opt => opt.Ignore())
                    .ForMember(domain => domain.EntityId, opt => opt.Ignore())
                    .ForMember(domain => domain.Entity, opt => opt.Ignore())
                    .ForMember(domain => domain.DataType, opt => opt.Ignore())
                    .ForMember(domain => domain.DataTypeName, opt => opt.MapFrom( model => model.DataType))
                    .ReverseMap();
            });
        }
    }
}
