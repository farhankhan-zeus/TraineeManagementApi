
using RabbitMQ.Client;

namespace TraineeManagementApi.utils;

public static class HealthCheckConfig{
 
 public static IServiceCollection AddHealthChecksExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddMySql(
                configuration.GetConnectionString("DefaultConnection")!,
                name: "mysql",
                tags: new [] {"ready"}
            )
            .AddRedis(
                configuration.GetConnectionString("RedisConnection")!,
                name: "redis",
                tags: new [] {"ready"}
            )
            .AddRabbitMQ(
                async sp => await sp.GetRequiredService<ConnectionFactory>().CreateConnectionAsync(),
                name: "RabbitMQ",
                tags: new [] {"ready"}
            );


        return services;
    }
    }