using System.Linq;
using System.ComponentModel.DataAnnotations;
using Tests.Unit.Domain.Utils;
using Xunit;
using Domain.Entities.EntityAggregate;
using Domain.Core.Extensions;
using Domain.Entities;
using Domain.Core.ValueObjects;
using Infrastructure.CrossCutting.Notifications;
using Domain.ValueObjects;

namespace Tests.Unit.Domain.Entities
{
    public class AttributeDomainTest
    {
        [Fact]
        public void AttributeIdentifier()
        {
            var attribute = new AttributeDomain(new Name("Id"), EnumDataTypes.Identifier);
            var notifications = new NotificationManager();

            var valid = attribute.IsValid(notifications);
            
            Assert.True(valid);
        }


        [Fact]
        public void AttributeInvalidName()
        {
            var attribute = new AttributeDomain(null, EnumDataTypes.Identifier);

            var notifications = new NotificationManager();
            var valid = attribute.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(AttributeDomain.Name), notifications.ToPropertiesNameList());
        }       

        [Fact]
        public void AttributeInvalidIdentifierName()
        {
            var attribute = new AttributeDomain(new Name("code"), EnumDataTypes.Identifier);

            var notifications = new NotificationManager();
            var valid = attribute.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(AttributeDomain.Name), notifications.ToPropertiesNameList());
        }  

        [Fact]
        public void AttributeInvalidDataType()
        {
            var attribute = new AttributeDomain(new Name("Name"), EnumDataTypes.Null);

            var notifications = new NotificationManager();
            var valid = attribute.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(AttributeDomain.DataType), notifications.ToPropertiesNameList());
        }           

        [Fact]
        public void AttributeStringWithLength()
        {
            var attribute = new AttributeDomain(
                new Name("Name"), 
                EnumDataTypes.String,
                true,
                64
            );

            var notifications = new NotificationManager();
            var valid = attribute.IsValid(notifications);
            
            Assert.True(valid);
        }  

        [Fact]
        public void AttributeStringWithoutLength()
        {
            var attribute = new AttributeDomain(new Name("Name"), EnumDataTypes.String);

            var notifications = new NotificationManager();
            var valid = attribute.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(AttributeDomain.Length), notifications.ToPropertiesNameList());
        }                   

    }
}