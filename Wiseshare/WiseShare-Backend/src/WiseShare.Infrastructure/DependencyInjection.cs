
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WiseShare.Application.Common.Interfaces.Services;
using WiseShare.Infrastructure.Authentication;
using WiseShare.Infrastructure.Services;

namespace WiseShare.Infrastructure;

public static class DependencyInjection
{
   public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
     

        services.Configure<JwtSettings>(configuration.GetSection("jwtSettings"));
        services.AddSingleton<JwtTokenGenerator, JwtTokenGenerator>();

      services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}