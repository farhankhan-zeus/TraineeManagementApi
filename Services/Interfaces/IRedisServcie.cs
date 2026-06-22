


namespace TraineeManagementApi.Services.Interfaces;
public interface IRedisService
{
  public bool Trygetvalue<T> (string key,out T? value);

  public  Task SetAsync<T> (string key, T value,CancellationToken cancellationToken);

  public Task<T?> GetorSetAsync<T> (string key,Func<Task<T?>> factory,CancellationToken cancellationToken);

  public  Task InvalidateAsync<T> (string key, CancellationToken cancellationToken);
}