using Domain.Services;
using GraphQL;
using GraphQL.Types;
using System;
using System.Threading.Tasks;

namespace Infrastructure.GraphQL
{
    public class GraphQLRepository
    {
        private IServiceProvider _serviceProvider;
        private IDynamicDomainService _dynamicService;
        private Schema _schema;

        public GraphQLRepository(IServiceProvider serviceProvider, IDynamicDomainService dynamicService)
        {
            _serviceProvider = serviceProvider;
            _dynamicService = dynamicService;

            CreateGraphQLSchema();
        }

        private void CreateGraphQLSchema()
        {
            var queryObject = new EntityDynamicQuery(_serviceProvider);
            foreach (var type in _dynamicService.Entities)
                queryObject.AddQuery(type);

            _schema = new Schema()
            {
                Query = queryObject
            };
        }

        public async Task<ExecutionResult> Query(GraphQLQuery query)
        {
            var inputs = query.Variables.ToInputs();

            return await new DocumentExecuter().ExecuteAsync(_ =>
            {
                _.Schema = _schema;
                _.Query = query.Query;
                _.OperationName = query.OperationName;
                _.Inputs = inputs;
            }).ConfigureAwait(false);
        }
    }
}
