using FluentMigrator.Runner;

using Infrastructure.Access.Repositories;
using Infrastructure.Access.Repositories.Abstractions;
using Infrastructure.Common.DatabaseProvider;

using Infrastructure.Migrations;
using Infrastructure.Notes.Repositories;
using Infrastructure.Notes.Repositories.Abstractions;

using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DepedencyInjection 
{
    public static IServiceCollection AddInfrasturcture(this IServiceCollection services)
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
        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<ILabelRepository, LabelRepository>();

        return services;
    }
}