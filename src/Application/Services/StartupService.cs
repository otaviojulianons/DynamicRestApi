using Application.Commands;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Entities.EntityAggregate;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StartupService
    {

        private IServiceScope _serviceScope;
        private IDynamicDomainService _dynamicService;
        private ILogger<StartupService> _logger;
        private IRepository<EntityDomain> _entityRepository;

        public StartupService(IServiceProvider serviceProvider)
        {
            _serviceScope = serviceProvider.CreateScope();
            _entityRepository = _serviceScope.ServiceProvider.GetService<IRepository<EntityDomain>>();
            _dynamicService = _serviceScope.ServiceProvider.GetService<IDynamicDomainService>();
            _logger = _serviceScope.ServiceProvider.GetService<ILogger<StartupService>>();
        }

        public async Task Start()
        {
            try
            {
                var entities = _entityRepository.GetAll();
                _dynamicService.GenerateType(entities.ToArray());
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
