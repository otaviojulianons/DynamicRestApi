using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Structure;
using Domain.Entities.LanguageAggregate;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.ValueObjects
{   
    public abstract class DataTypeAbstract : 
        AbstractValidator<DataTypeAbstract>, IValueObject<DataTypeAbstract> , IDataType
    {
        public DataTypeAbstract(EnumDataTypes dataType, string name) 
            : this(dataType, name, null)
        {
        }

        public DataTypeAbstract(EnumDataTypes dataType, string name, string format)
        {
            DataType = dataType;
            Name =  name;
            Format = format ?? name;

            RuleFor(obj => obj.Name).NotEmpty();
        }             
        
        public EnumDataTypes DataType { get; private set; }
        public string Name { get; private set; }
        public string Format { get; private set; }

        public bool Equals(DataTypeAbstract other)
        {
            return Name.Equals(other.Name) && Format.Equals(other.Format);
        }

        public override string ToString() => Name;

    }
}
