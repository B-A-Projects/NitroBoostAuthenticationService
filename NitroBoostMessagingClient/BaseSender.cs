using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using NitroBoostMessagingClient.Interfaces;

namespace NitroBoostMessagingClient;

public class BaseSender : BaseMessager, IBaseSender
{
    public BaseSender(string hostName, string userName = "", string password = "", int port = 5432, string virtualHost = "/")
    : base(hostName, userName, password, port, virtualHost) {}

    public void Send<TType>(string queueName, TType body) where TType : class
    {
        if (!TryConnect())
            return;
        
        using IModel channel = RabbitmqConnection.CreateModel();
        channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        byte[] content = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueName,
            basicProperties: null,
            body: content);
    }

    private bool TryConnect() => (RabbitmqConnection?.IsOpen).GetValueOrDefault() || Connect();
}