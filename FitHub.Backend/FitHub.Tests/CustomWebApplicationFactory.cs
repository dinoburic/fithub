
using FitHub.Application.Modules.Auth.Commands.Login;
using System.Net.Http.Headers;
using System.Net.Http.Json;

[assembly: Xunit.CollectionBehavior(DisableTestParallelization = true)]
namespace FitHub.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<Program>
{
    private static string? _cachedToken;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");
    }

    public async Task<HttpClient> GetAuthenticatedClientAsync()
    {
        var client = CreateClient();
        if (string.IsNullOrEmpty(_cachedToken))
        {
            var loginRequest = new
            {
                Email = "string@string.com",
                Password = "@String123"
            };

            var response = await client.PostAsJsonAsync("api/auth/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var loginResponse = await response.Content.ReadFromJsonAsync<LoginCommandDto>();
            _cachedToken = loginResponse.AccessToken;
        }
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _cachedToken);
        return client;
    }
}