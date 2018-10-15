﻿using Domain.Core.Interfaces.Structure;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Core.Implementation
{
    public abstract class AggregateRoot : IAggregateRoot
    {
        public virtual long Id { get; }

        [NotMapped]
        public IList<INotification> Notifications { get; private set; } = new List<INotification>();

        protected void AddNotification(INotification notification)
        {
            Notifications.Add(notification);
        }
    }
}