using Domain.Core.Interfaces.Structure;

namespace Infrastructure.Dynamic
{
    public interface IDynamicEntity<TId, TModel> : IGenericEntity<TId>
    {
        new TId Id { get; set; }

        void Map(TId id, TModel model);
    }
}
