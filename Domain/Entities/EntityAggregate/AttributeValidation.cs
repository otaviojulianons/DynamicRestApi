using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeValidation : AbstractValidator<AttributeDomain>
    {
        public AttributeValidation()
        {
            RuleFor(item => item.Name).NotEmpty();
            RuleFor(item => item.DataTypeId)
                .NotEqual(0)
                .WithMessage((item,value) => $"DataType '{item.DataTypeName}' not found!");
        }
    }
}
