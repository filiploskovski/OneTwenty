using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OneTwenty.Infrastructure;

public static class DbInit
{
    public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseString = configuration["ConnectionStrings:DatabaseConnection"];

        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(databaseString, sql =>
                {
                })
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging();
        });
    }
}