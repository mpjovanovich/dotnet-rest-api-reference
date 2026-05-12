using DotnetRestApiReference.Infrastructure.Seeder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetRestApiReference.Infrastructure.Extensions;

public static class DevelopmentDataSeedingExtensions
{
    public static IServiceCollection AddDevelopmentDataSeeding(
        this IServiceCollection services,
        IHostEnvironment environment)
    {
        if (!environment.IsDevelopment())
        {
            return services;
        }

        services.AddHostedService<DevelopmentDataSeedHostedService>();
        return services;
    }
}
