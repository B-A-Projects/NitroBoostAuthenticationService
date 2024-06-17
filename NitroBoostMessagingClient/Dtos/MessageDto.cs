using NitroBoostMessagingClient.Enums;

namespace NitroBoostMessagingClient.Dtos;

public class MessageDto
{
    public ActionType Action { get; set; }
    public string Sender { get; set; }
    public string Body { get; set; }
    public string Token { get; set; }
}