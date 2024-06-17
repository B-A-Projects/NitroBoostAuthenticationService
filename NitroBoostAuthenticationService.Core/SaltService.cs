using System.Security.Cryptography;
using System.Text;
using NitroBoostAuthenticationService.Shared.Dtos;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;

namespace NitroBoostAuthenticationService.Core;

public class SaltService : ISaltService
{
    private ISaltRepository _repository;
    
    private static readonly int _keySize = 128;
    private static readonly int _iterationCount = 50000;

    public SaltService(ISaltRepository repository) => _repository = repository;

    public async Task<List<byte[]>?> Hash(long userId, List<string> values)
    {
        var salt = await _repository.GetSalt(userId);
        if (salt == null)
            return null;

        var hashes = new List<byte[]>();
        values.ForEach(x => hashes.Add(GenerateHash(x, salt.Salt)));
        return hashes;
    }

    public async Task<List<bool>?> Validate(long userId, List<string> values, List<byte[]> hashes)
    {
        if (values.Count != hashes.Count)
            return null;
        
        var salt = await _repository.GetSalt(userId);
        return salt == null ? null : values.Select((t, i) => HashValuesMatch(t, hashes[i], salt.Salt)).ToList();
    }

    public async Task AddSalt(long userId)
    {
        if (await _repository.SaltExists(userId))
            return;

        SaltDto dto = new SaltDto()
        {
            UserId = userId,
            Salt = RandomNumberGenerator.GetBytes(_keySize)
        };
        await _repository.AddSalt(dto);
    }

    public async Task DeleteSalt(long userId) => await _repository.DeleteSalt(userId);

    private byte[] GenerateHash(string input, byte[] salt) => Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(input),
            salt,
            _iterationCount,
            HashAlgorithmName.SHA512,
            _keySize);

    private bool HashValuesMatch(string input, byte[] hash, byte[] salt)
    {
        var inputHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(input),
            salt,
            _iterationCount,
            HashAlgorithmName.SHA512,
            _keySize);
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
}