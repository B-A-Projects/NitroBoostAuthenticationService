using NitroBoostAuthenticationService.Core;
using NitroBoostAuthenticationService.Data;
using NitroBoostAuthenticationService.Data.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;
using NitroBoostMessagingClient.Dtos;
using NitroBoostMessagingClient.Enums;
using NitroBoostMessagingClient.Interfaces;

namespace NitroBoostAuthenticationService.Messaging;

public class MessageProcessor : IMessageProcessor
{
    private ISaltService _service;
    private TokenHelper _helper;

    public MessageProcessor(NitroBoostAuthenticationContext context, TokenHelper helper)
    {
        _service = new SaltService(new SaltRepository(context));
        _helper = helper;
    }

    public async Task ProcessMessage(MessageDto message)
    {
        try
        {
            if (!_helper.ValidateSender(message.Token, message.Body))
                return;
        
            switch (message.Action)
            {
                case ActionType.Add:
                    await _service.AddSalt(long.Parse(message.Body));
                    break;
                case ActionType.Delete:
                    await _service.DeleteSalt(long.Parse(message.Body));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        catch (Exception e)
        {
            //TODO: LOG PROPERLY
            Console.WriteLine(e);
            throw;
        }
    }
}