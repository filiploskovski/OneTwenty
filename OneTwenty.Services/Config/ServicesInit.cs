using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OneTwenty.Services.Services;
using OneTwenty.Services.Services.Interest;
using OneTwenty.Services.Services.User;

namespace OneTwenty.Services.Config;

public static class ServicesInit
{
    public static void AddOneTwentyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDuplicateUsersService, DuplicateUsersService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IInterestService, InterestService>();
    }
}
