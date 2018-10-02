using MediatR;
using System.Collections.Generic;

namespace Domain.Interfaces.Structure
{
    public interface IEntity
    {
        long Id { get; }
    }
}