using MediatR;
using System;

namespace Application.Commands
{
    public class DeleteEntityCommand : IRequest<bool>
    {
        public Guid Id { get; private set; }

        public DeleteEntityCommand(Guid id)
        {
            Id = id;
        }
    }
}
