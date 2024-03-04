using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTwenty.Jobs.Jobs.Csv;
using OneTwenty.Jobs.Jobs.RestApi.RefitClients;
using Refit;

namespace OneTwenty.Jobs.Jobs.RestApi.Config;

public static class RestApiInit
{
    public static void AddRestApiJob(this IServiceCollection services, IConfiguration configuration)
    {
        var baseAddress = configuration["ExternalUsersApi"]!;
        
        services.AddRefitClient<IUsersApi>()
            .ConfigureHttpClient(q => q.BaseAddress = new Uri(baseAddress));
        
        services.AddScoped<IRestApiJobService, RestApiJobService>();
    }
}