using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using TraineeManagementApi.Models;
using TraineeManagementApi.Services.Interfaces;
using TraineeManagementApi.Constants;

namespace TraineeManagementApi.Services;

public class RabbitMQService:IRabbitMQService
{

    private readonly IConfiguration _config;
    private readonly ILogger<RabbitMQService> _logger;
    private readonly ConnectionFactory _connection;
    public RabbitMQService(IConfiguration configuration, ILogger<RabbitMQService> logger,ConnectionFactory connection)
    {
        _config = configuration;
        _logger = logger;
        _connection=connection;
    }
    public async Task SendMessage<T> (T message, MessageBus messagebus,CancellationToken cancellationToken)
    {
       
              
        // _connection.AutomaticRecoveryEnabled=true;
        IConnection connection = await _connection.CreateConnectionAsync();      
        using var Channel = await connection.CreateChannelAsync();
         await Channel.QueueDeclareAsync(
            queue: RabbitMQConstants.QUEUE_NAME,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments:  new Dictionary<string, object?>
            {
                ["x-dead-letter-exchange"]= RabbitMQConstants.X_DEAD_LETTER_EXCHANGE,
                ["x-dead-letter-routing-key"] =RabbitMQConstants.X_DEAD_LETTER_EXCHANGE_KEY
            },
            cancellationToken: cancellationToken
        );

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