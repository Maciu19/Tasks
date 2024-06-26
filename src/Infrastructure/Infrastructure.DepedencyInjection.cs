using FluentMigrator.Runner;

using Infrastructure.Common.DatabaseProvider;

using Infrastructure.Migrations;
using Infrastructure.Repositories;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjection 
{
    public static IServiceCollection AddInfrasturcture(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSingleton<IDatabaseProvider, DatabaseProvider>()
            .AddMigration()
            .AddRepositories();

        return services;
    }

    public static IServiceCollection AddMigration(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var databaseProviderService = serviceProvider.GetRequiredService<IDatabaseProvider>();

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString(databaseProviderService.GetDefaultConnectionString())
                .ScanIn(typeof(CreateUserTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}