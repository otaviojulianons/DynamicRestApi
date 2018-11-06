using Domain.Core.Validators;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.EntityAggregate
{
    public class EntityValidator : AbstractValidator<EntityDomain>
    {
        public EntityValidator()
        {
            RuleFor(item => item.Name).Valid();

            RuleFor(item => item.Attributes)
                .Must(ContainsMoreThanOneAttribute)
                .Must(ContainsAttributeIdentifier)
                .SetCollectionValidator(new AttributeValidator())
                .WithMessage("Invalid attributes.");

        }

        private bool ContainsMoreThanOneAttribute(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes?.Count > 1;

        private bool ContainsAttributeIdentifier(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes.Any(item => item.IsIdentifier);
    }
}
