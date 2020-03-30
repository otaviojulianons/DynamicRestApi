using Application.Commands;
using Application.Queries;
using AutoMapper;
using Common.Extensions;
using Domain.Core.ValueObjects;
using Domain.Entities.EntityAggregate;

namespace Application
{
    public static class AutoMapper
    {
        public static void Register()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AttributeDto, AttributeDomain>(MemberList.None)
                    .ConstructUsing(model => 
                        new AttributeDomain(model.Name, model.DataType, model.AllowNull, model.Length))
                    .ReverseMap();

                cfg.CreateMap<ElementDto, ElementDomain>(MemberList.None)
                    .ConstructUsing((model, context) =>
                    {
                        var type = model.DataType;
                        var entity = context.Mapper.Map<EntityDomain>(model.Entity);
                        return new ElementDomain(entity, type);
                    }).ReverseMap();

                cfg.CreateMap<CreateEntityCommand, EntityDomain>(MemberList.None)
                    .ConstructUsing((model, context) =>
                    {
                        var entityDomain = new EntityDomain(new Name(model.Name));
                        model?.Attributes.ForEach(attribute =>
                        {
                            var attributeDomain = context.Mapper.Map<AttributeDomain>(attribute);
                            entityDomain.AddAttribute(attributeDomain);
                        });
                        model?.Elements.ForEach(element =>
                        {
                            var elementDomain = context.Mapper.Map<ElementDomain>(element);
                            entityDomain.AddElement(elementDomain);
                        });
                        return entityDomain;
                    }).ReverseMap();

                cfg.CreateMap<EntityDomain, EntityQueryResult>(MemberList.Destination);
                cfg.CreateMap<AttributeDomain, AttributeQueryResult>(MemberList.Destination);
                cfg.CreateMap<ElementDomain, ElementQueryResult>(MemberList.Destination);

            });
        }
    }
}
