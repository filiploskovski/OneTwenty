using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OneTwenty.Jobs.Jobs.Csv;
using OneTwenty.Jobs.Jobs.RestApi;

namespace OneTwenty.Jobs.Config;

public class JobsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRecurringJobManager _recurringJobManager;

    public JobsMiddleware(RequestDelegate next, IRecurringJobManager recurringJobManager)
    {
        _next = next;
        _recurringJobManager = recurringJobManager;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _recurringJobManager.AddOrUpdate<IRestApiJobService>("From Rest API", (x) => x.Execute() , Cron.Minutely());
        _recurringJobManager.AddOrUpdate<ICsvJobService>("From CSV", (x) => x.Execute() , Cron.Minutely());
        await _next(context);
    }
}

public static class JobsMiddlewareExtensions
{
    public static IApplicationBuilder UseJobsMiddleware(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<JobsMiddleware>();
        builder.UseHangfireDashboard();
        return builder;
    }
}