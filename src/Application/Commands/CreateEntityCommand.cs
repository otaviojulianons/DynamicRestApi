using MediatR;
using System.Collections.Generic;

namespace Application.Commands
{
    public class CreateEntityCommand : IRequest<bool>
    {

        public string Name { get; set; }
        
        public IEnumerable<AttributeDto> Attributes { get; set; }

        public IEnumerable<ElementDto> Elements { get; set; }

    }
}
