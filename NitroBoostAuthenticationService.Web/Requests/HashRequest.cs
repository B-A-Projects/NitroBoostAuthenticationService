namespace NitroBoostAuthenticationService.Web.Requests;

public class HashRequest
{
    public long UserId { get; set; }
    public List<string> Values { get; set; }
}