using Microsoft.Extensions.DependencyInjection;
using WebApi.CrossCutting.UserActivityLogging;

namespace WebApi.CrossCutting
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddCrossCutting(this IServiceCollection services)
        {
            return services.AddScoped<LogUserActivityActionFilter>();
        }
    }
}
