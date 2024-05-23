using Cooking.Application;
using Cooking.Application.Abstractions.Caching;
using Cooking.Application.Abstractions.Clock;
using Cooking.Application.Abstractions.Data;
using Cooking.Domain.Abstractions;
using Cooking.Domain.Categories;
using Cooking.Domain.Comments;
using Cooking.Domain.Ingredients;
using Cooking.Domain.Products;
using Cooking.Domain.Recipes;
using Cooking.Domain.Users;
using Cooking.Infrastructure.Caching;
using Cooking.Infrastructure.Clock;
using Cooking.Infrastructure.Data;
using Cooking.Infrastructure.Outbox;
using Cooking.Infrastructure.Repositories;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Cooking.Infrastructure;

public static class InfrastructureSettings
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connectionString,
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                    .LogTo(Console.WriteLine, LogLevel.Information));

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IIngredientRepository, IngredientRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRecipeRepository, RecipeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));

        services.AddSingleton<ISqlOutBoxConnectionFactory>(_ => new SqlOutBoxConnectionFactory(connectionString));

        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

        //SqlMapper.AddTypeHandler(typeof(List<Item>), new JsonObjectTypeHandler());

        //SqlMapper.AddTypeHandler(typeof(Address), new JsonObjectTypeHandler());

        services.AddApplication();

        services.AddCaching(configuration);

        return services;
    }

    public static IServiceCollection AddOutboxMessages(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();

        services.AddQuartz();

        return services;
    }

    public static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache")
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();

        return services;
    }
}
