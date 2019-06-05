using FluentValidation;
using System;

namespace Domain.Core.Interfaces.Structure
{
    public interface IValueObject<T> : IValidator<T>, IEquatable<T>
    {

    }
}
