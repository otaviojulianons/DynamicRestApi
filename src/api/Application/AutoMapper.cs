using Application.Models;
using AutoMapper;
using Domain.Entities.EntityAggregate;
using Domain.Entities;
using Domain.Core.ValueObjects;
using Application.Commands;
using Domain.ValueObjects;

namespace Application
{
    public static class AutoMapper
    {
        public static void MapperRegister()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Attribute, AttributeDomain>(MemberList.None)
                    .ForMember(model => model.Name, opt => opt.MapFrom(x => new Name(x.Name) ))
                    .ReverseMap();

                cfg.CreateMap<Entity, EntityDomain>(MemberList.None)
                    .ForMember(domain => domain.Name, opt => opt.MapFrom( x => new Name(x.Name)))
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.Attributes, opt => opt.Ignore())
                    .ReverseMap();

                cfg.CreateMap<CreateEntityCommand, EntityDomain>(MemberList.None)
                    .ForMember(domain => domain.Name, opt => opt.MapFrom( x => new Name(x.Name)))
                    .ForMember(domain => domain.Id, opt => opt.Ignore())
                    .ForMember(domain => domain.Attributes, opt => opt.Ignore())
                    .ReverseMap();                    
            });
        }
    }
}
