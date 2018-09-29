using FluentValidation;
using System;

namespace Domain.Interfaces.Structure
{
    public interface IValueObject<T> : IValidator<T>, IEquatable<T>
    {

    }
}
