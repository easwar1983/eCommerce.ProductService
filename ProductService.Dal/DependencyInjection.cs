using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Dal.Context;
using ProductService.Dal.Repository;
using ProductService.Dal.RepositoryContracts;

namespace ProductService.Dal;
public static class DependencyInjection
{
    /// <summary>
    /// Extension  method to add infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="iocServices"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection iocServices,IConfiguration configuration)
    {
        // Register your Data Access Layer (DAL) services here
        // Example: services.AddSingleton<IMyService, MyService>();

        iocServices.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(configuration.GetConnectionString("DefaultConnection")!));

        iocServices.AddScoped<IProductsRepository,ProductsRepository>();

        //iocServices.AddTransient<DapperDbContext>();
        //iocServices.AddTransient<IProductRepository, ProductRepository>();

        return iocServices;
    }
}
