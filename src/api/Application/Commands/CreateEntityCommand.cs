using Application.Models;
using MediatR;

namespace Application.Commands
{
    public class CreateEntityCommand : IRequest<bool>
    {
        public Entity Entity { get; private set; }

        public CreateEntityCommand(Entity entity)
        {
            Entity = entity;
        }

    }
}
