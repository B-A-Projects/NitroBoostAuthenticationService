namespace NitroBoostAuthenticationService.Shared.Interfaces.Repositories;

public interface IAuthenticationRepository
{
    Task<AccountDto?> CreateAccount(AccountDto account);
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
    Task<AccountDto?> GetAccountById(long id);
    Task<AccountDto?> GetAccountByNickname(string uniqueNickname);
    Task<AccountDto?> GetAccountByEmail(string email);
}