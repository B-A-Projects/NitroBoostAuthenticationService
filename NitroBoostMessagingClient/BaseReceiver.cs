using System.Text;
using Newtonsoft.Json;
using NitroBoostMessagingClient.Dtos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using NitroBoostMessagingClient.Interfaces;

namespace NitroBoostMessagingClient;

public class BaseReceiver : BaseMessager, IBaseReceiver
{
    private IMessageProcessor _processor;
    
    private IModel? _channel;
    private EventingBasicConsumer _consumer;
    private string _queueName;

    public BaseReceiver(IMessageProcessor processor, string hostName, string queueName, string userName = "",
        string password = "", int port = 5432,
        string virtualHost = "/")
        : base(hostName, userName, password, port, virtualHost)
    {
        _processor = processor;
        _queueName = queueName;
    }

    public void StartReceiving()
    {
        if ((RabbitmqConnection?.IsOpen).GetValueOrDefault() || !TryConnect() || _queueName.Length == 0 || DataReceived == null)
            return;

        _channel = RabbitmqConnection.CreateModel();
        _channel.QueueDeclare(
            queue: _queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _consumer = new EventingBasicConsumer(_channel);
        _consumer.Received += DataReceived;
        _channel.BasicConsume(_queueName, false, _consumer);
    }

    public void StopReceiving()
    {
        if ((RabbitmqConnection?.IsOpen).GetValueOrDefault())
            Disconnect();
    }
    
    private bool TryConnect() => (RabbitmqConnection?.IsOpen).GetValueOrDefault() || Connect();
    
    private void DataReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        _processor.ProcessMessage(JsonConvert.DeserializeObject<MessageDto>(
            Encoding.UTF8.GetString(eventArgs.Body.ToArray()
            )));
        _channel?.BasicAck(eventArgs.DeliveryTag, false);
    }
    
    public override void Dispose()
    {
        StopReceiving();
        base.Dispose();
    }
}