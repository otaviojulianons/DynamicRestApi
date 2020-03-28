using Common.Notifications;
using Domain.Core.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Dynamic
{
    [Route("[controller]")]
    public class TestDynamicController : DynamicEntityController<TestEntity, int, TestModel> 
    {
        public TestDynamicController(
            INotificationManager msgs,
            IGenericRepository<TestEntity, int> genericRepository) 
        : base(msgs, genericRepository)
        {
        }
    }
}
