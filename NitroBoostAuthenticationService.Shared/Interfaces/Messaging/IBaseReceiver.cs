using RabbitMQ.Client.Events;

namespace NitroBoostAuthenticationService.Shared.Interfaces.Messaging;

public interface IBaseReceiver
{
    event EventHandler<BasicDeliverEventArgs>? DataReceived;
    
    void StartReceiving(string queueName);
    void StopReceiving();
}