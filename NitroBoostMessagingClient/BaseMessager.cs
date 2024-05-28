using RabbitMQ.Client;

namespace NitroBoostMessagingClient;

public abstract class BaseMessager : IDisposable
{
    public ConnectionFactory RabbitmqConnectionFactory { get; private set; }

    protected IConnection? RabbitmqConnection;

    public BaseMessager(string hostName, string userName, string password, int port, string virtualHost) => RabbitmqConnectionFactory = 
        new ConnectionFactory()
        {
            HostName = hostName
        };
    
    protected bool Connect()
    {
        if ((RabbitmqConnection?.IsOpen).GetValueOrDefault())
            return true;
        
        int attemptCounter = 0;
        do
        {
            RabbitmqConnection = RabbitmqConnectionFactory.CreateConnection();
            RabbitmqConnection.ConnectionShutdown += Shutdown;
        } while (attemptCounter++ < 30 & (RabbitmqConnection?.IsOpen).GetValueOrDefault());

        return (RabbitmqConnection?.IsOpen).GetValueOrDefault();
    }

    protected void Disconnect()
    {
        if (!(RabbitmqConnection?.IsOpen).GetValueOrDefault())
            return; 
        
        RabbitmqConnection.Abort();
        RabbitmqConnection = null;
    }

    private void Shutdown(object? sender, ShutdownEventArgs e) => Disconnect();

    public virtual void Dispose() => Disconnect();
}