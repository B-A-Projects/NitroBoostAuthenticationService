using NitroBoostAuthenticationService.Data.Entities;
using NitroBoostAuthenticationService.Shared;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;

namespace NitroBoostAuthenticationService.Data.Repositories;

public class AuthenticationRepository : 
    BaseRepository<Account>, IAuthenticationRepository
{
    public AuthenticationRepository(NitroBoostAuthenticationContext context) : base(context) {}
    
    public async Task<AccountDto?> CreateAccount(AccountDto account)
    {
        Account entity = new Account()
        {
            Email = account.Email.ToLower(),
            Password = account.Password,
            Salt = account.Salt,
            UserRole = account.UserRole
        };
        await Add(entity);
        await _context.SaveChangesAsync();
        return entity.ToSafeDto();
    }

    public async Task<bool> EmailExists(string email) => 
        (await Find(x => x.Email == email.ToLower())).Any();

    public async Task<bool> UsernameExists(string username) => 
        (await Find(x => x.UniqueUsername == username.ToLower())).Any();

    public async Task<AccountDto?> GetAccountById(long id) => 
        (await Find(x => x.Id == id)).FirstOrDefault()?.ToDto();

    public async Task<AccountDto?> GetAccountByNickname(string uniqueNickname) => 
        (await Find(x => x.UniqueUsername == uniqueNickname.ToLower())).FirstOrDefault()?.ToDto();

    public async Task<AccountDto?> GetAccountByEmail(string email) => 
        (await Find(x => x.Email == email)).FirstOrDefault()?.ToDto();

    public async Task<bool> DeleteAccount(long accountId)
    {
        Account? account = (await Find(x => x.Id == accountId)).FirstOrDefault();
        if (account == null)
            return false;
        Delete(account);
        await _context.SaveChangesAsync();
        return true;
    }
}