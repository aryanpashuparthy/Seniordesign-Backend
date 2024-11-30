using Microsoft.Extensions.DependencyInjection;
using Wiseshare.Application.services;
using Wiseshare.Application.services.UserServices;
using WiseShare.Application.Authentication;

namespace WiseShare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services
        services.AddSingleton<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
