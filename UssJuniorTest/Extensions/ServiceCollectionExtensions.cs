using UssJuniorTest.Infrastructure.Repositories;
using UssJuniorTest.Infrastructure.Services.DrivesLogsAggregationService;
using UssJuniorTest.Infrastructure.Store;

namespace UssJuniorTest.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterAppServices(this IServiceCollection services)
    {
        services.AddSingleton<IStore, InMemoryStore>();

        services.AddScoped<CarRepository>();
        services.AddScoped<PersonRepository>();
        services.AddScoped<DriveLogRepository>();

        services.AddScoped<DrivesLogsAggregationService>();
    }
}