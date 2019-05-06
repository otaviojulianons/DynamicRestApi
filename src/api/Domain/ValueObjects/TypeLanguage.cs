using Domain.Core.Interfaces.Structure;
using Domain.Entities.LanguageAggregate;
using FluentValidation;

namespace Domain.ValueObjects
{
    public class TypeLanguage : AbstractValidator<TypeLanguage>, IValueObject<TypeLanguage>
    {
        public TypeLanguage(LanguageDataTypeDomain dataType, bool nullable)
        {
            Type =  nullable ? dataType.NameNullable ?? dataType.Name : dataType.Name;
            Format = dataType.Format;

            RuleFor(obj => obj.Type).NotEmpty();
            RuleFor(obj => obj.Format).NotEmpty();
        }
        
        public string Type { get; private set; }
        public string Format { get; private set; }

        public bool Equals(TypeLanguage other)
        {
            return Type.Equals(other.Type) && Format.Equals(other.Format);
        }
    }
}
