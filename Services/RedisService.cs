using System.Text.Json;
using System.Text.Json.Serialization;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using TraineeManagementApi.Services.Interfaces;
namespace TraineeManagementApi.Services;

public class RedisService:IRedisService{
     private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = null,
        WriteIndented = false,
        AllowTrailingCommas = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    private readonly ILogger<RedisService> _logger;
    private readonly IDistributedCache _cache;

    public RedisService(ILogger<RedisService> logger,IDistributedCache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    private static readonly TimeSpan slidingexpiration = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan absoluteceiling = TimeSpan.FromHours(1);

    private readonly DistributedCacheEntryOptions cachingoptions= new DistributedCacheEntryOptions().SetSlidingExpiration(slidingexpiration).SetAbsoluteExpiration(absoluteceiling);

    public  bool Trygetvalue <T> (string key, out T? value)
    {       
        value=default;
       
        try
        {
            string? response =_cache.GetString(key);
        if(response is null) return false;
         value = JsonSerializer.Deserialize<T>(response,SerializerOptions);
        if(value is null) return false;
        return true;
        }
        catch (RedisConnectionException)
        {
            
            _logger.LogWarning("Cache unavailable");
            return false;
        }

        
    }

    public async Task SetAsync <T> (string key, T value,CancellationToken cancellationToken)
    { 
       
        string valuedata = JsonSerializer.Serialize(value,SerializerOptions);
        await _cache.SetStringAsync(key,valuedata,cachingoptions,cancellationToken);
        _logger.LogInformation($"Cache set for key: {key}",key);

        
    }

    public async Task<T?>  GetorSetAsync<T> (string key, Func<Task<T?>> factory,CancellationToken cancellationToken)
    {
        if(Trygetvalue<T>(key,out T? value) && value is not null)
        {   
            _logger.LogInformation($"Cache hit for key : {key}",key);
            return value;
        }

        _logger.LogWarning($"Cache Miss for key:{key} ; Trying Database ",key);

        value = await factory();
        try
        {
            if(value is not null)
        {
            
           
            await SetAsync<T>(key,value,cancellationToken);
        }
        }
        catch (RedisConnectionException)
        {
            
            _logger.LogWarning("Cache unavailable");
        }
        return value;
        
        
    }

    public async Task InvalidateAsync<T> (string key, CancellationToken cancellationToken)
    {
        
        try
        {
        await _cache.RemoveAsync(key,cancellationToken);
        _logger.LogInformation($"Invalidating the cache for key: {key}",key);
            
        }
        catch (RedisConnectionException)
        {
        _logger.LogWarning("Cache unavailable");
        }
        
    }

}