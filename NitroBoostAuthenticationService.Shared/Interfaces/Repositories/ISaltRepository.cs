using NitroBoostAuthenticationService.Shared.Dtos;

namespace NitroBoostAuthenticationService.Shared.Interfaces.Repositories;

public interface ISaltRepository
{
    Task<bool> SaltExists(long userId);
    Task<SaltDto?> GetSalt(long userId);
    Task AddSalt(SaltDto dto);
    Task DeleteSalt(long userId);
}