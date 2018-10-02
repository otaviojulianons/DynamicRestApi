using Domain.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Application.Services
{
    public class StartAppService
    {
        public static async Task Run(IServiceScopeFactory serviceFactory)
        {
            using (var scope = serviceFactory.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var mediator = provider.GetService<IMediator>();

                await mediator.Publish(new GenerateDynamicObjectsEvent());
            }
        }
    }
}
