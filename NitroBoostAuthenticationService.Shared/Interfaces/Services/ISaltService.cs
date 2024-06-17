namespace NitroBoostAuthenticationService.Shared.Interfaces.Services;

public interface ISaltService
{
    Task<List<byte[]>?> Hash(long userId, List<string> values);
    Task<List<bool>?> Validate(long userId, List<string> values, List<byte[]> hashes);
    Task AddSalt(long userId);
    Task DeleteSalt(long userId);
}