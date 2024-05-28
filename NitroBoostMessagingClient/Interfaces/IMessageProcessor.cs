using NitroBoostMessagingClient.Dtos;

namespace NitroBoostMessagingClient.Interfaces;

public interface IMessageProcessor
{
    Task ProcessMessage(MessageDto message);
}