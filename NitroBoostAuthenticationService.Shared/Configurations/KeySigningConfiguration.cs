namespace NitroBoostAuthenticationService.Shared.Configurations;

public class KeySigningConfiguration
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}