
using Azure.Core;
using Azure.Identity;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace myCustomResource.services
{
  public class AzureManagement
  {
    private HttpClient _httpClient;

    public AzureManagement()
    {
      _httpClient = new HttpClient();
      _httpClient.BaseAddress = new Uri("https://management.azure.com/");
      var tokenCredential = new DefaultAzureCredential();
      var accessToken = tokenCredential.GetToken(
          new TokenRequestContext(scopes: new string[] { "https://management.azure.com/.default" }) { }
      );
      _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken.Token);
    }

    public async Task RestartAppService(string appServiceResourceId)
    {
      var url = appServiceResourceId + "/restart?api-version=2021-02-01";
      var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
      var response = await _httpClient.SendAsync(requestMessage);
      if (!response.IsSuccessStatusCode) { throw new Exception("Failed to restart web app." + response.ReasonPhrase); }
    }
  }
}
