using DotnetRestApiReference.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotnetRestApiReference.Infrastructure.Seeder;

internal sealed class DevelopmentDataSeedHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        DevDataSeeder.Seed(
            serviceProvider.GetRequiredService<IRegionsRepository>(),
            serviceProvider.GetRequiredService<IBirdsRepository>());
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
