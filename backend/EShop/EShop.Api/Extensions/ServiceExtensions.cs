using EShop.Contracts;
using EShop.Repository;
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
        
}
