using IdentityModel.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

var discoClient = new HttpClient();

var disco = await discoClient.GetDiscoveryDocumentAsync("https://localhost:7000");

if (disco.IsError)
{
    Console.WriteLine(disco.Error);
    return;
}

var httpTokenClient = new HttpClient()
{
    BaseAddress = new Uri(disco.TokenEndpoint ?? "")
};

var httpTokenClientOptions = new TokenClientOptions { ClientId = "vlad", ClientSecret = "secret" };

TokenClient tokenClient = new TokenClient(httpTokenClient, httpTokenClientOptions);

var tokenResponse = await tokenClient.RequestClientCredentialsTokenAsync("bankOfDotNetApi");

if (tokenResponse.IsError)
{
    Console.WriteLine(tokenResponse.Error);
    return;
}

Console.WriteLine(tokenResponse.Json);
Console.WriteLine("\n\n");

var apiClient = new HttpClient()
{
    BaseAddress = new Uri("http://localhost:5001")
};

apiClient.SetBearerToken(tokenResponse.AccessToken ?? "");

var customerInfo = new StringContent(JsonConvert.SerializeObject(new { FirstName="Manish", LastName="Narayan"}), Encoding.UTF8, "application/json");

var createCustomerResponse = await apiClient.PostAsync(apiClient.BaseAddress + "api/customers", customerInfo);

if (!createCustomerResponse.IsSuccessStatusCode)
{
    Console.WriteLine(createCustomerResponse.StatusCode);
}

var getCustomerResponse = await apiClient.GetAsync(apiClient.BaseAddress + "api/customers");

if (!getCustomerResponse.IsSuccessStatusCode)
{
    Console.WriteLine(createCustomerResponse.StatusCode);
}
else
{
    var content = await getCustomerResponse.Content.ReadAsStringAsync();

    Console.WriteLine(JArray.Parse(content));
}

Console.ReadLine();
