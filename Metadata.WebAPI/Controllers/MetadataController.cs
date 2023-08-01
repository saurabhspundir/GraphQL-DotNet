using GraphQL;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Transport;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace Metadata.WebAPI.Controllers
{
  [ApiController]
  [Route("/api/[controller]")]
  public class MetadataController : Controller
  {
    private readonly ISchema _schema;
    private readonly IDocumentExecuter _documentExecuter;

    public MetadataController(ISchema schema,IDocumentExecuter documentExecuter)
    {
      _schema = schema;
      _documentExecuter = documentExecuter;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GraphQLRequest graphQlRequest)
    {
      var executionOptions = new ExecutionOptions()
      {
        Schema = _schema,
        Query = graphQlRequest.Query,
        OperationName = graphQlRequest.OperationName,
        Variables= graphQlRequest.Variables
      };
      return new ExecutionResultActionResult(await _documentExecuter.ExecuteAsync(executionOptions));
    }
  }
}
