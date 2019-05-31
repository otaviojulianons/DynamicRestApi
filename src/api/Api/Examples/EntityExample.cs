using Application.Commands;
using Application.Models;
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
                Attributes = new List<Attribute>()
                {
                    new Attribute()
                    {
                        Name = "Id",
                        DataType = EnumDataTypes.Identifier
                    },
                    new Attribute()
                    {
                        Name = "Name",
                        DataType = EnumDataTypes.String,
                        Length = 64
                    }
                }
            };

        }
    }
}
