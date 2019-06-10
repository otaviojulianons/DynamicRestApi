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
                    .WithMessage("Invalid amount of attributes.")
                .Must(ContainsOnlyOneIdentifier)
                    .WithMessage("It is mandatory to have only one identifier attribute.");

            RuleForEach(item => item.Attributes)
                .SetValidator(new AttributeValidator());

        }

        private bool ContainsMoreThanOneAttribute(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes?.Count > 1;

        private bool ContainsOnlyOneIdentifier(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes.Where(item => item.IsIdentifier)?.Count() == 1;            
    }
}
