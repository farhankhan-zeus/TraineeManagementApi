
using TraineeManagementApi.Models;

namespace TraineeManagementApi.Services.Interfaces;
public interface IRabbitMQService
{
    public Task SendMessage<T> (T message, MessageBus messageBus ,CancellationToken cancellationToken);
}