using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using myCustomResource.services;
using System.Net;

namespace myCustomResource
{
    public class Actions
    {
        private readonly ILogger<Actions> _logger;
        private readonly AzureManagement _azureManagement;

        public Actions(ILogger<Actions> logger, AzureManagement azureManagement)
        {
            _logger = logger;
            _azureManagement  = azureManagement;
        }

        [Function("Restart")]
        public async Task<HttpResponseData> Restart([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    
        {
          _logger.LogInformation("C# HTTP trigger function processed a request.");

          try
          {
            var webApp1Id = System.Environment.GetEnvironmentVariable("WEBAPP1_ID");
            var webApp2Id = System.Environment.GetEnvironmentVariable("WEBAPP2_ID");
            await _azureManagement.RestartAppService(webApp1Id);
            await _azureManagement.RestartAppService(webApp2Id);
            return req.CreateResponse(HttpStatusCode.OK);
            //return new OkObjectResult("Welcome to Azure Functions!");
          }
          catch (Exception ex) {
            _logger.LogError(ex, "Whooooooops");
            return req.CreateResponse(HttpStatusCode.InternalServerError);
          }
        }
    }
}
