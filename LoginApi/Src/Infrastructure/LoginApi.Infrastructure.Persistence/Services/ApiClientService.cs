using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HyBrForex.Infrastructure.Persistence.Services;

public class ApiClientService
{
    private readonly HttpClient _httpClient;

    public ApiClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> GetAsync<T>(string endpoint)
    {
        var response = await _httpClient.GetAsync(endpoint);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}