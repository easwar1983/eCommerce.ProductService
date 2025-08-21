using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Bal.ServiceContract;
using ProductService.Bal.Services;
using ProductService.Bal.Validators;

namespace ProductService.Bal;
    public static class DependencyInjection
    {
    /// <summary>
    /// Extension  method to add infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="iocServices"></param>
    /// <returns></returns>
    public static IServiceCollection AddBusinessAccessLayer(this IServiceCollection iocServices)
    {
        // Register your Business Access Layer (BAL) services here
        // Example: services.AddSingleton<IMyService, MyService>();

        iocServices.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(ProductService.Bal.Mappers.ProductMapping).Assembly);
        });

        iocServices.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        iocServices.AddScoped<IProductService, ProductService.Bal.Services.ProductService>();

        return iocServices;
    }
}
