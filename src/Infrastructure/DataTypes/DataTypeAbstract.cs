using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Core.Interfaces.Structure;
using Domain.ValueObjects;
using FluentValidation;
using FluentValidation.Results;

namespace InfrastructureTypes
{   
    public abstract class DataTypeAbstract : 
        AbstractValidator<DataTypeAbstract>, IValueObject<DataTypeAbstract> , IDataType
    {
        public DataTypeAbstract(EnumDataTypes dataType, string name) 
            : this(dataType, name, null)
        {
        }

        public DataTypeAbstract(EnumDataTypes dataType, string name, string parameter)
        {
            DataType = dataType;
            Name =  name;
            Parameter = parameter;

            RuleFor(obj => obj.Name).NotEmpty();
        }             
        
        public EnumDataTypes DataType { get; private set; }
        public string Name { get; private set; }
        public string Parameter { get; private set; }

        public bool Equals(DataTypeAbstract other)
        {
            return Name.Equals(other.Name) && Parameter.Equals(other.Parameter);
        }

        public override string ToString() => Name;

    }
}
