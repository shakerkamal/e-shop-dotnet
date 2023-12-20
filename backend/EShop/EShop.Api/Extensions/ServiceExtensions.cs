using EShop.Application;
using EShop.Contracts;
using EShop.Entities.ConfigurationModels;
using EShop.LoggerService;
using EShop.Repository;
using EShop.Repository.Implementations;
using EShop.Services.Contracts;
using EShop.Services.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EShop.Api.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
    {
        services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value
            );
    }

    public static void ConfigureCors(this IServiceCollection services) =>
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void ConfigureMediatR(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));
    }

    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void ConfigureLogging(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerManager, LoggerManager>();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

        var secretKey = Environment.GetEnvironmentVariable("SECRET");

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidAudience = jwtConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };
        });
    }

    public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

}
