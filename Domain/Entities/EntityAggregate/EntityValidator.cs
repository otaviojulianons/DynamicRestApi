using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities.EntityAggregate
{
    public class EntityValidator : AbstractValidator<EntityDomain>
    {
        public EntityValidator()
        {
            RuleFor(item => item.Name).NotEmpty();

            RuleFor(item => item.Attributes)
                .Must(ContainsAttributes)
                .Must(ContainsAttributeIdentifier)
                .WithMessage("Invalid attributes.");
        }

        private bool ContainsAttributes(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes?.Count != 0;

        private bool ContainsAttributeIdentifier(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes.Any(item => item.IsIdentifier);
    }
}
