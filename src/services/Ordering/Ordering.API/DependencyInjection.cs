namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPIServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddCarter();
        return services;
    }

    public static WebApplication UseAPIServices(this WebApplication app)
    {
        app.MapCarter();
        return app;
    }
}
