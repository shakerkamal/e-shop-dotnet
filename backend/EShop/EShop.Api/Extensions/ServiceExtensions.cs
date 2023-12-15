using EShop.Application;
using EShop.Contracts;
using EShop.Repository;
using EShop.Repository.Implementations;
using Microsoft.Extensions.Options;

namespace EShop.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
    {
        services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<IMongoDbSettings>>().Value
            );
    }

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
    }

}
