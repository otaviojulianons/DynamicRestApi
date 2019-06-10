using Domain.Core.Validators;
using Domain.Core.ValueObjects;
using Domain.ValueObjects;
using FluentValidation;

namespace Domain.Entities.EntityAggregate
{
    public class AttributeValidator : AbstractValidator<AttributeDomain>
    {
        public AttributeValidator()
        {
            RuleFor(item => item.Name)
                .Must(ValidIdentifierName)
                .When(IdentifierDataType)
                .Valid();

            RuleFor(item => item.DataType)
                .NotEmpty()
                .NotEqual(EnumDataTypes.Null)
                .WithMessage((item, value) => $"DataType for '{item.Name}' not found!");

            RuleFor(item => item.Length)
                .NotEmpty()
                .When(StringDataType);                
        }

        private bool StringDataType(AttributeDomain attribute) =>
            attribute.DataType == EnumDataTypes.String;

        private bool IdentifierDataType(AttributeDomain attribute) =>
            attribute.DataType == EnumDataTypes.Identifier; 
                       
        private bool ValidIdentifierName(Name name) =>
            name?.ToString()?.ToLower() == "id";


    }
}
