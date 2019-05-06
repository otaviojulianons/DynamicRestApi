using Application.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Api.Examples
{
    public class EntityExample : IExamplesProvider<Entity>
    {
        public Entity GetExamples()
        {
            return new Entity()
            {
                Name = "EntityName",
                Attributes = new List<Attribute>()
                {
                    new Attribute()
                    {
                        Name = "Id",
                        DataType = "guid"
                    },
                    new Attribute()
                    {
                        Name = "Name",
                        DataType = "string",
                        Length = 64
                    }
                }
            };

        }
    }
}
