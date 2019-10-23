using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using Domain.Interfaces.Infrastructure;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Data.GraphQL
{
    public class EntityDynamicQuery : ObjectGraphType
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public EntityDynamicQuery(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
            Name = "RootQuery";
        }

        public void AddQuery<T>() where T : IEntity => AddQuery(typeof(T));

        public void AddQuery(Type type)
        {
            if (!type.GetInterfaces().Contains(typeof(IEntity)))
                throw new Exception($"Type {type.Name} not implemented IEntity interface.");

            dynamic repositoryType = typeof(IRepository<>).MakeGenericType(type);
            var repository = ServiceProvider.GetService(repositoryType);

            var entityGraphType = typeof(EntityType<>).MakeGenericType(type);
            var typeResult = typeof(ListGraphType<>).MakeGenericType(entityGraphType);

            this.Field(typeResult, type.Name,
                arguments: new QueryArguments(new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<string>("id");
                    if (id != null)
                    {
                        dynamic typeListResult = typeof(List<>).MakeGenericType(type);
                        var listResult = Activator.CreateInstance(typeListResult);

                        var idGuid = new Guid(id);
                        var item = repository.GetById(idGuid);               
                        if (item != null)
                            listResult.Add(item);

                        return listResult;
                    }
                    else
                        return repository.GetAll();
            });
        }
    }
}