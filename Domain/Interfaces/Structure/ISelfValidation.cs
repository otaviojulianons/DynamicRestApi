using FluentValidation;

namespace Domain.Interfaces.Structure
{
    public interface ISelfValidation<TEntity> : IEntity
    {
        IValidator<TEntity> Validator { get; }
    }
}
