using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OneTwenty.Jobs.Jobs.Csv.Config;

public static class CsvJobInit
{
    public static void AddCsvJob(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICsvJobService, CsvJobService>();
    }
}