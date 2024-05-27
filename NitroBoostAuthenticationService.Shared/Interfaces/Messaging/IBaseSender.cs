namespace NitroBoostAuthenticationService.Shared.Interfaces.Messaging;

public interface IBaseSender
{
    void Send<TType>(string queueName, TType body) where TType : class;
}