
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wiseshare.Application.Common.Interfaces.Authentication;
using Wiseshare.Application.Repository;
using WiseShare.Application.Common.Interfaces.Services;
using WiseShare.Infrastructure.Authentication;
using WiseShare.Infrastructure.Repository;
using WiseShare.Infrastructure.Services;

namespace WiseShare.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       ConfigurationManager configuration)
  {
    //test
    services.AddSingleton<IUserRepository, InMemoryUserRepository>();
    services.AddSingleton<IPropertyRepository, InMemoryPropertyRepository>();
    //real

    services.Configure<JwtSettings>(configuration.GetSection("jwtSettings"));
    services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();


    services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

    return services;
  }
}