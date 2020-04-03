using Common.Extensions;
using Domain.Core.Interfaces.Infrastructure;
using Domain.Core.Interfaces.Structure;
using GraphQL.Types;
using Infrastructure.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.GraphQL
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
            if (!type.ImplementsGericType(typeof(IGenericEntity<>)))
                throw new Exception($"Type {type.Name} not implemented IGenericEntity interface.");

            var interfaceType = type.GetInterfaces().FirstOrDefault();
            var typeId = interfaceType.GetGenericArguments().FirstOrDefault();
            var repositoryType = typeof(IGenericRepository<,>).MakeGenericType(type, typeId);
            var entityGraphType = typeof(EntityType<,>).MakeGenericType(type, typeId);
            var typeResult = typeof(ListGraphType<>).MakeGenericType(entityGraphType);

            dynamic repository = ServiceProvider.GetService(repositoryType);

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