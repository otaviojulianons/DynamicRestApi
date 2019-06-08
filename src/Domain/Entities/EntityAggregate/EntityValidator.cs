﻿using Domain.Core.Validators;
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
                .Must(ContainsAttributeIdentifier)
                    .WithMessage("Attribute identifier not found.");

            RuleForEach(item => item.Attributes)
                .SetValidator(new AttributeValidator());

        }

        private bool ContainsMoreThanOneAttribute(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes?.Count > 1;

        private bool ContainsAttributeIdentifier(IReadOnlyCollection<AttributeDomain> attributes)
            => attributes.Any(item => item.IsIdentifier);
    }
}