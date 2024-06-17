namespace NitroBoostAuthenticationService.Shared.Dtos;

public class SaltDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public byte[] Salt { get; set; }
}