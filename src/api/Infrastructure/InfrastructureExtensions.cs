using Application.Common.Cryptography;
using Application.Common.Identity;
using Application.Common.Persistence;
using Application.Common.Persistence.Photos;
using Domain.Aggregates.Users;
using Infrastructure.Cryptography;
using Infrastructure.Identity;
using Infrastructure.ObjectStorage;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatingAppDbContext>(opts =>
            {
                opts.UseNpgsql(configuration.GetConnectionString("Default"), npgsqlOpts => npgsqlOpts.UseNodaTime());
            });

            services.AddScoped<IDatingAppDbContext>(f => f.GetRequiredService<DatingAppDbContext>());
            services.AddScoped<IUnitOfWork>(f => f.GetRequiredService<DatingAppDbContext>());

            services
                .AddRepositories()
                .AddPhotoStorage(configuration);

            services.AddSingleton<IPasswordHashService, PasswordHashService>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddSingleton<IClock>(_ => SystemClock.Instance);

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IUserRepository, UserRepository>();
        }

        private static IServiceCollection AddPhotoStorage(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("ObjectStorage").Get<ObjectStorageSettings>();

            return services.AddSingleton<IPhotoStorage, PhotoStorage>(_ => new PhotoStorage(settings));
        }
    }
}
