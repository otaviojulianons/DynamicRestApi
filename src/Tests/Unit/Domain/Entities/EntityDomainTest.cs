using Common.Notifications;
using Domain.Core.ValueObjects;
using Domain.Entities.EntityAggregate;
using Domain.ValueObjects;
using Tests.Unit.Domain.Utils;
using Xunit;

namespace Tests.Unit.Domain.Entities
{
    public class EntityDomainTest
    {
        [Fact]
        public void ValidEntity()
        {
            var entity = GetValidEntityWithAttribute();
            var notifications = new NotificationManager();

            var valid = entity.IsValid(notifications);
            
            Assert.True(valid);
        }

        [Fact]
        public void InvalidEntityName()
        {
            var entity = new EntityDomain(null);
            AddAttributes(entity);

            var notifications = new NotificationManager();
            var valid = entity.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(EntityDomain.Name), notifications.ToPropertiesNameList());
        }   

        [Fact]
        public void EntityWithoutIdentifierAttribute()
        {
            var entity = new EntityDomain("EntityTest");
            entity.AddAttribute(GetNameAttribute());

            var notifications = new NotificationManager();
            var valid = entity.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(EntityDomain.Attributes), notifications.ToPropertiesNameList());
        }

        [Fact]
        public void EntityWithInvalidAmountAttributes()
        {
            var entity = new EntityDomain("EntityTest");
            entity.AddAttribute(GetIdentifierAttribute());

            var notifications = new NotificationManager();
            var valid = entity.IsValid(notifications);

            Assert.False(valid);
            Assert.Contains(nameof(EntityDomain.Attributes), notifications.ToPropertiesNameList());
        }        

        private EntityDomain GetValidEntityWithAttribute()
        {
            var entity = new EntityDomain("EntityTest");
            AddAttributes(entity);
            return entity;
        }

        private void AddAttributes(EntityDomain entity)
        {
            entity.AddAttribute(GetIdentifierAttribute());
            entity.AddAttribute(GetNameAttribute());
        }

        private AttributeDomain GetIdentifierAttribute() =>
            new AttributeDomain(new Name("Id"), EnumDataTypes.Identifier);

        private AttributeDomain GetNameAttribute() =>
            new AttributeDomain(new Name("Name"), EnumDataTypes.String, false, 64);

    }
}