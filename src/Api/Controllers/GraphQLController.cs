using Infrastructure.Data.GraphQL;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        private GraphQLRepository _graphQLRepository;

        public GraphQLController(GraphQLRepository graphQLRepository)
        {
            _graphQLRepository = graphQLRepository;
        }

        public async Task<IActionResult> Post([FromBody]GraphQLQuery query)
        {
            var result = await _graphQLRepository.Query(query);
            if (result.Errors?.Count > 0)
                return BadRequest();

            return Ok(result);
        }
    }
}
