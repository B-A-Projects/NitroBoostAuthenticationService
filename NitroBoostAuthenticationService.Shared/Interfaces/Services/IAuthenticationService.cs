namespace NitroBoostAuthenticationService.Shared.Interfaces.Services;

public interface IAuthenticationService
{
    Task<AccountDto?> CreateAccount(long profileId, string email, string password);
    Task<string?> Authenticate(string username, string password);
}