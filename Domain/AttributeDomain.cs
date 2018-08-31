using SharedKernel.Repository;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class AttributeDomain : Navigable, IEntity, IDomainValidation
    {

        public long Id { get; set; }
        public long EntityId { get; set; }
        public long DataTypeId { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
        public bool AllowNull { get; set; }

        [NotMapped]
        public string DataTypeName { get; set; }

        public DataTypeDomain DataType { get; set; }
        public EntityDomain Entity { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception($"Attribute name '{Name}' is invalid!");
            if (DataTypeId == 0)
                throw new Exception($"DataType '{DataTypeName}' not found!");

        }
    }
}
