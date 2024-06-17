using NitroBoostAuthenticationService.Data.Entities;
using NitroBoostAuthenticationService.Shared.Dtos;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;

namespace NitroBoostAuthenticationService.Data.Repositories;

public class SaltRepository : BaseRepository<Salt>, ISaltRepository
{
    public SaltRepository(NitroBoostAuthenticationContext context) : base(context) {}

    public async Task<bool> SaltExists(long userId) => 
        await Any(x => x.UserId == userId);

    public async Task<SaltDto?> GetSalt(long userId) => 
        (await Find(x => x.UserId == userId)).FirstOrDefault()?.ToDto();

    public async Task AddSalt(SaltDto dto)
    {
        var entity = new Salt()
        {
            SaltBytes = dto.Salt,
            UserId = dto.UserId
        };
        await Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteSalt(long userId)
    {
        Salt? entity = (await Find(x => x.UserId == userId)).FirstOrDefault();
        if (entity != null)
        {
            Delete(entity);
            await _context.SaveChangesAsync();
        }
    }
}