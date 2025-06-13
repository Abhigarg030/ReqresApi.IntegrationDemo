using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

using ReqresApi.Client.Common;
using ReqresApi.Client.Models;

namespace ReqresApi.Client.Services;

public class ReqresApiClient
{
    private readonly HttpClient _httpClient;

    public ReqresApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto?> GetUserByIdAsync(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"users/{userId}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var wrapper = await response.Content.ReadFromJsonAsync<SingleUserResponse>();
            return wrapper?.Data;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            // Log or rethrow depending on use-case
            throw new ExternalApiException("Failed to connect to Reqres API", ex);
        }
        catch (NotSupportedException ex)
        {
            Console.WriteLine($"Unsupported media type: {ex.Message}");
            throw new ExternalApiException("Unsupported content type from Reqres API", ex);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
            throw new ExternalApiException("Invalid JSON returned from Reqres API", ex);
        }
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var allUsers = new List<UserDto>();
        int page = 1;

        try
        {
            while (true)
            {
                var response = await _httpClient.GetFromJsonAsync<UserListResponse>($"users?page={page}");

                if (response?.Data == null || response.Data.Count == 0)
                    break;

                allUsers.AddRange(response.Data);

                if (page >= response.Total_Pages)
                    break;

                page++;
            }

            return allUsers;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
            throw new ExternalApiException("Error fetching user list from Reqres API", ex);
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON error: {ex.Message}");
            throw new ExternalApiException("Error deserializing user list from Reqres API", ex);
        }
    }

    private class SingleUserResponse
    {
        public UserDto Data { get; set; }
    }
}
