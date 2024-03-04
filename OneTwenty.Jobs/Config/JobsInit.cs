using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTwenty.Jobs.Jobs.Csv;
using OneTwenty.Jobs.Jobs.RestApi;
using OneTwenty.Jobs.Jobs.RestApi.Config;

namespace OneTwenty.Jobs.Config;

public static class JobsInit
{
    public static void AddJobs(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseString = configuration["ConnectionStrings:DatabaseConnection"];

        services.AddScoped<ICsvJobService, CsvJobService>();

        services.AddRestApiJob(configuration);

        services.AddHangfire(config =>
            config.UseSqlServerStorage(databaseString));
        services.AddHangfireServer();
    }
}