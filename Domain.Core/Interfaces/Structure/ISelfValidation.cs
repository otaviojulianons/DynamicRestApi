using FluentValidation;

namespace Domain.Core.Interfaces.Structure
{
    public interface ISelfValidation<TEntity> : IEntity
    {
        IValidator<TEntity> Validator { get; }
    }
}
