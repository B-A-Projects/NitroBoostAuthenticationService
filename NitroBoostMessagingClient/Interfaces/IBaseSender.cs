namespace NitroBoostMessagingClient.Interfaces;

public interface IBaseSender
{
    void Send<TType>(string queueName, TType body) where TType : class;
}