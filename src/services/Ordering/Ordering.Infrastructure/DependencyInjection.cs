namespace Ordering.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        // var connectionString = configuration.GetConnectionString("Database");
        // services.AddDbContext<OrderContext>(options =>
        //     options.UseSqlServer(connectionString)
        // );
        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
