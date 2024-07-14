using Application.Services;

using Domain.Validators;

using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DepedencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddServices()
            .AddValidators();

        return services;
    }

    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<UserAddRequestValidator>();

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationService, AuthorizationService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<INoteService, NoteService>();

        return services;
    }
}