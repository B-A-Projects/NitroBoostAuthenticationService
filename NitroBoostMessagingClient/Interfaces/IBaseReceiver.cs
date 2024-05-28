using RabbitMQ.Client.Events;

namespace NitroBoostMessagingClient.Interfaces;

public interface IBaseReceiver
{
    void StartReceiving();
    void StopReceiving();
}