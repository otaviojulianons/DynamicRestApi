using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StartupService
    {

        private IServiceScope _serviceScope;
        private IMediator _mediator;
        private ILogger<StartupService> _logger;
        private IRepository<EntityDomain> _entityRepository;

        public StartupService(IServiceProvider serviceProvider)
        {
            _serviceScope = serviceProvider.CreateScope();
            _entityRepository = _serviceScope.ServiceProvider.GetService<IRepository<EntityDomain>>();
            _mediator = _serviceScope.ServiceProvider.GetService<IMediator>();
            _logger = _serviceScope.ServiceProvider.GetService<ILogger<StartupService>>();
        }

        public async Task Start()
        {
            try
            {
                var entities = _entityRepository.GetAll();
                await _mediator.Send(new CreateDynamicEndpointCommand(entities));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                _serviceScope?.Dispose();
            }

        }
    }
}
