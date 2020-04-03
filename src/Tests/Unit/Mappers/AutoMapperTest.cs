using Application.Commands;
using AutoMapper;
using Domain.Core.ValueObjects;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Unit.Mappers
{
    public class AutoMapperTest
    {
        public AutoMapperTest()
        {
            Application.AutoMapper.Register();
        }

        [Fact]
        public void MapperCreateEntityCommandToDomain()
        {
            var createEntityCommand = CreateCreateEntityCommand("EntityTest", true);
            var attributeDto = createEntityCommand?.Attributes?.FirstOrDefault();
            var elementDto = createEntityCommand?.Elements?.FirstOrDefault();
            var elementEntityDto = elementDto?.Entity;
            var elementAttributeDto = elementEntityDto?.Attributes?.FirstOrDefault();

            var entityDomain = Mapper.Map<EntityDomain>(createEntityCommand);
            var attributeDomain = entityDomain?.Attributes?.FirstOrDefault();
            var elementDomain = entityDomain?.Elements?.FirstOrDefault();
            var elementEntityDomain = elementDomain?.Entity;
            var elementAttributeDomain = elementEntityDomain?.Attributes?.FirstOrDefault();

            Assert.Equal(entityDomain?.Name, createEntityCommand.Name);
            Assert.Equal(attributeDomain?.Name, attributeDto.Name);
            Assert.Equal(attributeDomain?.DataType, attributeDto.DataType);
            Assert.Equal(attributeDomain?.AllowNull, attributeDto.AllowNull);
            Assert.Equal(attributeDomain?.Length, attributeDto.Length);
            Assert.Equal(elementDomain?.DataType, elementDto?.DataType);
            Assert.Equal(elementEntityDomain?.Name, elementEntityDto?.Name);
            Assert.Equal(elementAttributeDomain?.Name, elementAttributeDto?.Name);
            Assert.Equal(elementAttributeDomain?.DataType, elementAttributeDto?.DataType);
            Assert.Equal(elementAttributeDomain?.AllowNull, elementAttributeDto?.AllowNull);
            Assert.Equal(elementAttributeDomain?.Length, elementAttributeDto?.Length);
        }

        [Fact]
        public void MapperEntityDomainToDto()
        {
            var entityDomain = CreateEntityDomain("EntityDomain", true);
            var attributeDomain = entityDomain?.Attributes?.FirstOrDefault();
            var elementDomain = entityDomain?.Elements?.FirstOrDefault();
            var elementEntityDomain = elementDomain?.Entity;
            var elementAttributeDomain = elementEntityDomain?.Attributes?.FirstOrDefault();

            var entityDto = Mapper.Map<EntityDomain>(entityDomain);
            var attributeDto = entityDto?.Attributes?.FirstOrDefault();
            var elementDto = entityDto?.Elements?.FirstOrDefault();
            var elementEntityDto = elementDto?.Entity;
            var elementAttributeDto = elementEntityDto?.Attributes?.FirstOrDefault();

            Assert.Equal(entityDto?.Id, entityDomain.Id);
            Assert.Equal(entityDto?.Name, entityDomain.Name);
            Assert.Equal(attributeDto?.Name, attributeDomain.Name);
            Assert.Equal(attributeDto?.DataType, attributeDomain.DataType);
            Assert.Equal(attributeDto?.AllowNull, attributeDomain.AllowNull);
            Assert.Equal(attributeDto?.Length, attributeDomain.Length);
            Assert.Equal(elementDto?.DataType, elementDomain?.DataType);
            Assert.Equal(elementEntityDto?.Name, elementEntityDomain?.Name);
            Assert.Equal(elementAttributeDto?.Name, elementAttributeDomain?.Name);
            Assert.Equal(elementAttributeDto?.DataType, elementAttributeDomain?.DataType);
            Assert.Equal(elementAttributeDto?.AllowNull, elementAttributeDomain?.AllowNull);
            Assert.Equal(elementAttributeDto?.Length, elementAttributeDomain?.Length);
        }

        private CreateEntityCommand CreateCreateEntityCommand(string entityName, bool addElement)
        {
            var attributeModel = new AttributeDto()
            {
                Name = "Id",
                DataType = EnumDataTypes.String,
                AllowNull = false,
                Length = 32
            };

            var command = new CreateEntityCommand()
            {
                Name = entityName,
                Attributes = new List<AttributeDto>() { attributeModel }
            };

            if (addElement)
            {
                var elementEntityCommand = CreateCreateEntityCommand("ElementChild", false);
                var elementDto = new ElementDto()
                {
                    Entity = elementEntityCommand,
                    DataType = EnumDataTypes.Object
                };
                command.Elements = new List<ElementDto>() { elementDto };
            }

            return command;
        }



        private EntityDomain CreateEntityDomain(string entityName, bool addElement)
        {
            var entityDomain = new EntityDomain(new Name(entityName));
            var attributeDomain = new AttributeDomain(
                new Name("Id"),
                EnumDataTypes.String,
                false,
                32
            );
            entityDomain.AddAttribute(attributeDomain);
            if(addElement)
            {
                var entityElementDomain = CreateEntityDomain("ElementChild", false);
                var element = new ElementDomain(entityElementDomain, EnumDataTypes.Object);
                entityDomain.AddElement(element);
            }
            return entityDomain;
        }
    }
}
