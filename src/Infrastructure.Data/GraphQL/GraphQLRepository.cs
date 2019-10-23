using Domain.Interfaces.Infrastructure;
using GraphQL;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data.GraphQL
{
    public class GraphQLRepository
    {
        private IServiceProvider _serviceProvider;
        private IDynamicService _dynamicService;

        public GraphQLRepository(IServiceProvider serviceProvider, IDynamicService dynamicService)
        {
            _serviceProvider = serviceProvider;
            _dynamicService = dynamicService;
        }

        public async Task<ExecutionResult> Query(GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();
            var queryObject = new EntityDynamicQuery(_serviceProvider);

            foreach (var type in _dynamicService.DynamicTypes)
                queryObject.AddQuery(type);

            var schema = new Schema()
            {
                Query = queryObject
            };

            return await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            }).ConfigureAwait(false);
        }
    }
}
