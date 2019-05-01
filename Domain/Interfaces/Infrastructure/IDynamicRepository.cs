using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;

namespace Domain.Interfaces.Infrastructure
{
    public interface IDynamicRepository<T> : IRepository<T> where T : IEntity
    {
    }
}
