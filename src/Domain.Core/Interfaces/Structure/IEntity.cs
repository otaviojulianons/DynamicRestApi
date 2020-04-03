using System;

namespace Domain.Core.Interfaces.Structure
{
    public interface IEntity : IGenericEntity<Guid>
    {
        Guid Id { get; }
    }
}