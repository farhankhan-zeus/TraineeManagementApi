using TraineeManagementApi.Services;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.utils;
 
public static class ServiceBuilder
{
    public static IServiceCollection applicationServices (this IServiceCollection services)
    {
        services.AddScoped<ITraineeService,TraineeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IMentorService,MentorService>();
        services.AddScoped<ILearningTaskService,LearningTaskService>();
        services.AddScoped<ITaskAssignmentService,TaskAssignmentService>();
        services.AddScoped<ISubmissionService,SubmissionService>();
        services.AddScoped<IReviewService,ReviewService>();
        services.AddScoped<IFileStorageService,FileStorageService>();
        services.AddScoped<IJobProcessing,JobProcessing>();
        services.AddScoped<IRedisService,RedisService>();
        services.AddScoped<IRabbitMQService,RabbitMQService>();
        return services;

    }
}