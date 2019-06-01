﻿using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Interfaces.Infrastructure;
using Domain.Models;
using Infrastructure.DataTypes.Factories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CommandHandlers
{
    public class CreateDynamicDocumentationCommandHandler : IRequestHandler<CreateDynamicDocumentationCommand, bool>
    {
        private IDynamicService _serviceDynamic;
        private IRepository<EntityDomain> _entityRepository;
        private IDocumentationRepository _swaggerRepository;

        public CreateDynamicDocumentationCommandHandler(
            IDynamicService serviceDynamic,
            IServiceProvider serviceProvider,
            IRepository<EntityDomain> entityRepository,
            IDocumentationRepository swaggerRepository
            )
        {
            _serviceDynamic = serviceDynamic;
            _entityRepository = entityRepository;
            _swaggerRepository = swaggerRepository;
        }

        public Task<bool> Handle(CreateDynamicDocumentationCommand request, CancellationToken cancellationToken)
        {
            var swaggerDataTypeFactory = new SwaggerDataTypeFactory();
            var entities = _entityRepository.GetAll();
            var entitiesTemplates = entities.Select(entity => new EntityTemplate(entity, swaggerDataTypeFactory)).ToArray();

            var json = _serviceDynamic.GenerateSwaggerJsonFile(entitiesTemplates);
            _swaggerRepository.Update(json);
            return Task.FromResult(true);
        }
    }
}