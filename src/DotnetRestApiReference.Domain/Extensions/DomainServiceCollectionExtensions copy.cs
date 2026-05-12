using DotnetRestApiReference.Domain.Interfaces.Services;
using DotnetRestApiReference.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetRestApiReference.Domain.Extensions;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddScoped<IRegionsService, RegionsService>();
        services.AddScoped<IBirdsService, BirdsService>();
        return services;
    }
}