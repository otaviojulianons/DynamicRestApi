using System.Collections.Generic;
using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class CreateEntityCommand : IRequest<bool>
    {

        public string Name { get; set; }
        
        public IEnumerable<Attribute> Attributes { get; set; }        

    }
}
