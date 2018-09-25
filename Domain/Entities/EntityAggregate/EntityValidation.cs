using FluentValidation;
using System.Collections.Generic;

namespace Domain.Entities.EntityAggregate
{
    public class EntityValidation : AbstractValidator<EntityDomain>
    {
        public EntityValidation()
        {
            RuleFor(item => item.Name).NotEmpty();

            RuleFor(item => item.Attributes)
                .Must(MustExistAttributes)
                .Must(MustContainsId)
                .WithMessage("Invalid attributes.");
        }

        private bool MustExistAttributes(List<AttributeDomain> attributes) 
            => attributes?.Count != 0;

        private bool MustContainsId(List<AttributeDomain> attributes) 
            => attributes.Find(item => item.Name == "Id" && item.DataTypeName == "long") == null;
    }
}
