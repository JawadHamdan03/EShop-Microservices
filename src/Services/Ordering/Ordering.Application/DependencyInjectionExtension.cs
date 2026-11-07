using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application;

public static class DependencyInjectionExtension
{

    public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
    {


        return services;
    }
    
    
}