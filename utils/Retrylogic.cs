namespace TraineeManagementApi.utils;

public static class Retrylogic
{
    public static async Task<T?> WithRetryAsync<T>(int attempts,Func<Task<T?>> factory,ILogger logger)
    {
        for(int i=1; i<=attempts; i++){
            try
            {
            T? response = await factory();
            return response;
            }
            catch(Exception e)
            {
                logger.LogWarning($"Attempt {i} failed for function: {factory.Method.Name}");
    
            }
        }
        return default;
    }
     public static T? WithRetry<T>(int attempts,Func<T?> factory,ILogger logger)
    {
        for(int i=1; i<=attempts; i++){
            try
            {
            T? response =  factory();
            return response;
            }
            catch(Exception e)
            {
                logger.LogWarning($"Attempt {i} failed for function: {factory.Method.Name}");
    
            }
        }
        return default;
    }

   
}