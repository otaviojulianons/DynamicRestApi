using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeValidator : AbstractValidator<AttributeDomain>
    {
        public AttributeValidator()
        {
            RuleFor(item => item.Name).NotEmpty();
            RuleFor(item => item.DataTypeId)
                .NotEqual(0)
                .WithMessage((item, value) => $"DataType '{item.DataTypeName}' not found!");
        }
    }
}
