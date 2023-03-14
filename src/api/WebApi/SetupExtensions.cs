using System.Text;
using WebApi.CrossCutting;
using Application;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.Realtime.ConnectedUsers;

namespace WebApi
{
    public static class SetupExtensions
    {
        public static IServiceCollection AddRequiredServices(this IServiceCollection services, IConfiguration configuration)
        {
            var identitySection = configuration.GetSection("Identity");
            var identitySettings = identitySection.Get<IdentitySettings>();
            services.Configure<IdentitySettings>(identitySection);

            services.AddSignalR();
            services.AddSingleton<IConnectedUsersTracker, InMemoryConnectedUsersTracker>();

            services.AddCrossCutting();

            services
                .AddInfrastructure(configuration)
                .AddApplicationServices()
                .AddIdentityServices(identitySettings);

            return services;
        }

        private static IServiceCollection AddIdentityServices(this IServiceCollection services, IdentitySettings identitySettings)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(identitySettings.TokenSymmetricSigningKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
