using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using DatabaseSampler.Application.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace DatabaseSampler.Functions
{
    public class GetLocationFunction
    {
        private readonly ILocationService _locationService;

        public GetLocationFunction(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [Function("GetLocation")]
        public async Task<HttpResponseData> GetLocation(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] 
            HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("HttpFunction");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            string payload;
            using (var streamReader = new StreamReader(req.Body))
            {
                payload = await streamReader.ReadToEndAsync();
            }

            logger.LogInformation($"Have payload {payload}");

            var postcode = payload;

            var data = await _locationService.LookupPostcodeAsync(postcode);

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "application/json");
            
            var json = JsonSerializer.Serialize(data);
            await response.WriteStringAsync(json);

            return response;
        }
    }
}
