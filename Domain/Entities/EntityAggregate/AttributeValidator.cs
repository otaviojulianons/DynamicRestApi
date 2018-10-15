using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeValidator : AbstractValidator<AttributeDomain>
    {
        public AttributeValidator()
        {
            RuleFor(item => item.Name).NotEmpty();
            RuleFor(item => item.DataType)
                .NotEmpty()
                .WithMessage((item, value) => $"DataType for '{item.Name}' not found!");
        }
    }
}
