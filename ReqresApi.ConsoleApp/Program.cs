using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReqresApi.Client;
using ReqresApi.Client.Services;
using ReqresApi.Application.Services;
using ReqresApi.Client.Models;

var host = Host.CreateDefaultBuilder()
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<ReqresApiOptions>(
            context.Configuration.GetSection(ReqresApiOptions.SectionName));

        services.AddHttpClient<ReqresApiClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<
                Microsoft.Extensions.Options.IOptions<ReqresApiOptions>>().Value;

            client.BaseAddress = new Uri(options.BaseUrl);
            client.DefaultRequestHeaders.Add("x-api-key", options.ApiKey);
        });

        services.AddTransient<ExternalUserService>();
    })
    .Build();

var userService = host.Services.GetRequiredService<ExternalUserService>();

var users = await userService.GetAllUsersAsync();

foreach (var user in users)
{
    Console.WriteLine($"{user.Id}: {user.FullName} ({user.Email})");
}
