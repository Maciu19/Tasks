using API.Middleware;

using Application.Common.JWT;

using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API;

public static class DepedencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();
        services.AddAuthorization();

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));  
        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}