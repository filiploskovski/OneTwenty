using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTwenty.Jobs.Jobs.Csv.Config;
using OneTwenty.Jobs.Jobs.RestApi.Config;

namespace OneTwenty.Jobs.Config;

public static class JobsInit
{
    public static void AddJobs(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseString = configuration["ConnectionStrings:DatabaseConnection"];

        services.AddScoped<IDataStoreService, DataStoreService>();

        services.AddRestApiJob(configuration);
        services.AddCsvJob(configuration);

        services.AddHangfire(config =>
            config.UseSqlServerStorage(databaseString));
        services.AddHangfireServer(q => q.WorkerCount = 1);
    }
}