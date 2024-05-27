using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using NitroBoostAuthenticationService.Shared.Interfaces.Messaging;

namespace NitroBoostAuthenticationService.Web.Messaging;

public class BaseReceiver : BaseMessager, IBaseReceiver
{
    public event EventHandler<BasicDeliverEventArgs>? DataReceived;

    private IModel? _channel;
    private EventingBasicConsumer _consumer;
    
    public BaseReceiver(string hostName, string userName = "", string password = "", int port = 5432, string virtualHost = "/")
        : base(hostName, userName, password, port, virtualHost) {}

    public void StartReceiving(string queueName)
    {
        if ((RabbitmqConnection?.IsOpen).GetValueOrDefault() || !TryConnect() || queueName.Length == 0 || DataReceived == null)
            return;

        _channel = RabbitmqConnection.CreateModel();
        _channel.QueueDeclare(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _consumer = new EventingBasicConsumer(_channel);
        _consumer.Received += DataReceived;
        _channel.BasicConsume(queueName, false, _consumer);
    }

    public void StopReceiving()
    {
        if ((RabbitmqConnection?.IsOpen).GetValueOrDefault())
            Disconnect();
    }
    
    private bool TryConnect() => (RabbitmqConnection?.IsOpen).GetValueOrDefault() || Connect();

    public void AcknowledgeMessage(BasicDeliverEventArgs e) =>
        _channel?.BasicAck(e.DeliveryTag, false);
    
    public override void Dispose()
    {
        StopReceiving();
        base.Dispose();
    }
}