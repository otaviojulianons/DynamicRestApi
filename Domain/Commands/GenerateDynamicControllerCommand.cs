using Domain.Entities.EntityAggregate;
using MediatR;

namespace Domain.Commands
{
    public class GenerateDynamicControllerCommand : INotification
    {
        public EntityDomain Entity { get; set; }
    }
}
