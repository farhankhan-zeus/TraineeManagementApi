using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;

namespace TraineeManagementApi.Services;

public class RabbitMQService:IRabbitMQService
{

    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMQService> _logger;
    public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger)
    {
        _config = configuration;
        _logger = logger;
    }
    public async Task SendMessage<T> (T message, MessageBus messagebus,CancellationToken cancellationToken)
    {
       
              var factory = new ConnectionFactory
        {
            HostName= _config["RabbitMQ:Host"],
            UserName = _config["RabbitMQ:UserName"],
            Password = _config["RabbitMQ:Password"],
            Port= Convert.ToInt32(_config["RabbitMQ:Port"])
        };
        factory.AutomaticRecoveryEnabled=true;
        IConnection connection = await factory.CreateConnectionAsync();
        
        using
        var Channel = await connection.CreateChannelAsync();
        await Channel.QueueDeclareAsync(messagebus.QueueName,false,false,false,null);

        string json = JsonConvert.SerializeObject(message);
        byte[] body = Encoding.UTF8.GetBytes(json);
        BasicProperties properties = new BasicProperties();
        properties.Persistent=true;

        try
        {
             await Channel.BasicPublishAsync(exchange: string.Empty, routingKey: messagebus.QueueName, body: new ReadOnlyMemory<byte>(body), cancellationToken);
        
        }
        catch (RabbitMQ.Client.Exceptions.PublishException)
        {
            _logger.LogWarning("Failed to send message to RabbitMQ");
            
        }

       await Channel.CloseAsync(cancellationToken);
        await connection.CloseAsync(cancellationToken);
        
     
   


    }
}