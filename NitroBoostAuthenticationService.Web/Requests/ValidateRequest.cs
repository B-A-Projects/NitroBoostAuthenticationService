namespace NitroBoostAuthenticationService.Web.Requests;

public class ValidateRequest
{
    public long UserId { get; set; }
    public List<string> Values { get; set; }
    public List<byte[]> Hashes { get; set; }
}