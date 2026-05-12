using DotnetRestApiReference.Domain.Interfaces.Repositories;
using DotnetRestApiReference.Infrastructure.SQLite;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetRestApiReference.Infrastructure.Extensions;

public static class SQLiteInfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddSQLiteInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRegionsRepository, SQLiteRegionsRepository>();
        services.AddScoped<IBirdsRepository, SQLiteBirdsRepository>();
        return services;
    }
}