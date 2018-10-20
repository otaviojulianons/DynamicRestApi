using Domain.Core.Interfaces.Structure;
using Domain.Core.Validators;
using FluentValidation;

namespace Domain.Core.ValueObjects
{
    public class Name : AbstractValidator<Name>, IValueObject<Name>
    {
        public Name(string value)
        {
            Value = value;

            RuleFor(x => x.Value).Name();
        }

        public string Value { get; private set; }

        public bool Equals(Name other)
        {
            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Name name) => name.Value;

        public static implicit operator Name(string value) => new Name(value);

    }
}
