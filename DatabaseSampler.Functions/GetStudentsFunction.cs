using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using DatabaseSampler.Application.Interfaces;

namespace DatabaseSampler.Functions
{
    public class GetStudentsFunction
    {
        private readonly IPostgresSqlService _postgresSqlService;

        public GetStudentsFunction(IPostgresSqlService postgresSqlService)
        {
            _postgresSqlService = postgresSqlService;
        }

        [FunctionName("GetStudents")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            
            var data = await _postgresSqlService.GetStudentsAsync();

            var serializedData = JsonSerializer.Serialize(data);
            return new JsonResult(serializedData);
        }
    }
}
