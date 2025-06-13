using ReqresApi.Application.Models;
using ReqresApi.Client.Services;

namespace ReqresApi.Application.Services;

public class ExternalUserService
{
    private readonly ReqresApiClient _client;

    public ExternalUserService(ReqresApiClient client)
    {
        _client = client;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var dto = await _client.GetUserByIdAsync(id);
        return dto == null ? null : new User
        {
            Id = dto.Id,
            Email = dto.Email,
            FullName = $"{dto.First_Name} {dto.Last_Name}"
        };
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var dtos = await _client.GetAllUsersAsync();
        return dtos.Select(dto => new User
        {
            Id = dto.Id,
            Email = dto.Email,
            FullName = $"{dto.First_Name} {dto.Last_Name}"
        }).ToList();
    }
}
