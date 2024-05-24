using NitroBoostAuthenticationService.Shared.Enums;

namespace NitroBoostAuthenticationService.Shared;

public class AccountDto
{
    public long Id { get; set; }
    public string? UniqueUsername { get; set; }
    public string Email { get; set; }
    public long ProfileId { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public Role UserRole { get; set; }
}