using JuanApp.BLL.Interfaces;
using JuanApp.BLL.Services;
using JuanApp.Core.Interfaces;
using JuanApp.DLL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JuanApp.BLL.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHomeReadRepository, HomeReadRepository>();
        services.AddScoped<IProductReadRepository, ProductReadRepository>();

        services.AddScoped<IHomeService, HomeService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICartService, CartService>();

        return services;
    }
}
