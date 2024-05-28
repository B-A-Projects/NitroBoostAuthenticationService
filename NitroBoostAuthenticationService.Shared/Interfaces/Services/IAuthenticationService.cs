namespace NitroBoostAuthenticationService.Shared.Interfaces.Services;

public interface IAuthenticationService
{
    Task<AccountDto?> CreateAccount(string email, string password);
    Task<string?> Authenticate(string username, string password);
}