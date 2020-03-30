using Application.Commands;
using Domain.ValueObjects;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Api.Examples
{
    public class EntityExample : IExamplesProvider<CreateEntityCommand>
    {
        public CreateEntityCommand GetExamples()
        {
            return new CreateEntityCommand()
            {
                Name = "EntityName",
                Attributes = new List<AttributeDto>()
                {
                    new AttributeDto()
                    {
                        Name = "Id",
                        DataType = EnumDataTypes.Identifier
                    },
                    new AttributeDto()
                    {
                        Name = "Name",
                        DataType = EnumDataTypes.String,
                        Length = 64
                    }
                },
                Elements = new List<ElementDto>()
                {
                    new ElementDto()
                    {
                        DataType = EnumElementType.Object,
                        Entity =  new CreateEntityCommand()
                        {
                            Name = "Child",
                            Attributes = new List<AttributeDto>()
                            {
                                new AttributeDto()
                                {
                                    Name = "Name",
                                    DataType = EnumDataTypes.String,
                                    Length = 64
                                }
                            },
                            Elements = new List<ElementDto>()
                        }
                    }
                }
            };

        }
    }
}
