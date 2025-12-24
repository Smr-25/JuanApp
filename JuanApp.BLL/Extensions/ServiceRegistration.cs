using JuanApp.BLL.Interfaces;
using JuanApp.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JuanApp.BLL.Extensions;

public static class ServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ILayoutService, LayoutService>();
        services.AddScoped<ISliderService, SliderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAdvantagesService, AdvantageService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<ISizeService, SizeService>();
        services.AddScoped<IBasketService, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ISubscriberService, SubscriberService>();
        services.AddScoped<IAdminProductService, AdminProductService>();
        services.AddScoped<IAdminCategoryService, AdminCategoryService>();
        services.AddScoped<IAdminColorService, AdminColorService>();
        services.AddScoped<IAdminSizeService, AdminSizeService>();
        services.AddScoped<IAdminSliderService, AdminSliderService>();
        services.AddScoped<IAdminAdvantageService, AdminAdvantageService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<IBlogService, BlogService>();
        services.AddScoped<ISettingService, SettingService>();
        services.AddScoped<IProductReviewService, ProductReviewService>();

        return services;
    }
}

