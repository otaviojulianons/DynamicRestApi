using System;

namespace Domain.Core.Interfaces.Structure
{
    public interface IGenericEntity<T>
    {
        T Id { get; }
    }
}