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
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection iocServices, IConfiguration configuration)
    {
        // Register your Data Access Layer (DAL) services here
        // Example: services.AddSingleton<IMyService, MyService>();


        ///Local DB Access
        //string connectionString = configuration.GetConnectionString("DefaultConnection")!;

        ///Container DB Access
        string connectionStringTemplate = configuration.GetConnectionString("DefaultConnection")!;

        string connectionString = connectionStringTemplate
            .Replace("$MYSQL_HOST", Environment.GetEnvironmentVariable("MYSQL_HOST"))
            .Replace("$MYSQL_PASSWORD", Environment.GetEnvironmentVariable("MYSQL_PASSWORD"))
        ;

        iocServices.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(connectionString));

        iocServices.AddScoped<IProductsRepository, ProductsRepository>();

        return iocServices;
    }
}
