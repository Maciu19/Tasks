using Application.Access.Services;
using Application.Access.Services.Abstractions;
using Application.Access.Validators;
using Application.Notes.Services;
using Application.Notes.Services.Abstractions;

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
        services.AddScoped<ILabelService, LabelService>();

        return services;
    }
}