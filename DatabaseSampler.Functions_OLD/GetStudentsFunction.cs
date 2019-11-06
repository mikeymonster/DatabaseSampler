using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using DatabaseSampler.Application.Models;

namespace DatabaseSampler.Functions
{
    public static class GetStudentsFunction
    {
        [FunctionName("GetStudents")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] 
            HttpRequest req,
            ILogger log
            )
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonSerializer.Deserialize(requestBody);
            //name = name ?? data?.name;

            //TODO: Get from service or repository
            var data = new List<Student>
            {
                new Student()
                {
                    Id = 1,
                    FirstName = "Bobby",
                    LastName = "Yang",
                    Created = DateTime.UtcNow
                }
            };

            //return name != null
            //    ? (ActionResult)new OkObjectResult($"Hello, {name}")
            //    : new BadRequestObjectResult("Please pass a name on the query string or in the request body");

            var serializedData = JsonSerializer.Serialize(data);
            return new JsonResult(serializedData);
        }
    }
}
