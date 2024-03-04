using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTwenty.Services.Services;

namespace OneTwenty.Services.Config;

public static class ServicesInit
{
    public static void AddOneTwentyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDuplicateUsersService, DuplicateUsersService>();
        services.AddScoped<IUserService, UserService>();
    }
}
