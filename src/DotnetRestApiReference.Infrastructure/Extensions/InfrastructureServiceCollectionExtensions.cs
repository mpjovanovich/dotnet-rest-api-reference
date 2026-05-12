using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Infrastructure.InMemory;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetRestApiReference.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        // Using Singleton because we are using an in-memory database.
        services.AddSingleton<IRegionsRepository, InMemoryRegionsRepository>();
        services.AddSingleton<IBirdsRepository, InMemoryBirdsRepository>();
        return services;
    }
}