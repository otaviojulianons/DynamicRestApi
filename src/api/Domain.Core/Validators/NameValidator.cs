using Domain.Core.Interfaces.Structure;
using Domain.Core.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Domain.Core.Validators
{
    public static class NameValidator
    {
        public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                    .MaximumLength(256)
                    .NotEmpty()
                    .Must(ValidName)
                    .WithMessage("Invalid Name!");
        }

        public static bool ValidName(string name) => new Regex(@"^[a-zA-Z0-9\\_\\?]+$").IsMatch(name);

    }
}
