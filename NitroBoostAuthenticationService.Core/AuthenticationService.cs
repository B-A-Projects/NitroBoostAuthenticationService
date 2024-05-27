using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NitroBoostAuthenticationService.Shared;
using NitroBoostAuthenticationService.Shared.Configurations;
using NitroBoostAuthenticationService.Shared.Enums;
using NitroBoostAuthenticationService.Shared.Interfaces.Repositories;
using NitroBoostAuthenticationService.Shared.Interfaces.Services;

namespace NitroBoostAuthenticationService.Core;

public class AuthenticationService : IAuthenticationService
{
    private KeySigningConfiguration _configuration;
    private IAuthenticationRepository _repository;

    private static readonly int _keySize = 128;
    private static readonly int _iterationCount = 50000;

    public AuthenticationService(KeySigningConfiguration configuration, IAuthenticationRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }
    
    public async Task<AccountDto?> CreateAccount(long profileId, string email, string password)
    {
        if (await _repository.EmailExists(email))
            throw new DuplicateNameException();

        byte[] salt = new byte[_keySize];
        AccountDto account = new AccountDto()
        {
            Email = email,
            Password = GenerateHash(password, out salt),
            Salt = salt,
            ProfileId = profileId,
            UserRole = Role.User
        };
        return await _repository.CreateAccount(account);
    }
    
    public async Task<string?> Authenticate(string username, string password)
    {
        AccountDto? account = null;
        if (username.Contains('@'))
            account = await _repository.GetAccountByEmail(username);
        else
            account = await _repository.GetAccountByNickname(username);

        if (account == null)
            return null;
        if (!HashValuesMatch(password, account.Password, account.Salt))
            return null;
        return GenerateToken(account);
    }

    private byte[] GenerateHash(string input, out byte[] salt)
    {
        salt = RandomNumberGenerator.GetBytes(_keySize);
        return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(input),
            salt,
            _iterationCount,
            HashAlgorithmName.SHA512,
            _keySize);
    }

    private bool HashValuesMatch(string input, byte[] hash, byte[] salt)
    {
        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(input),
            salt,
            _iterationCount,
            HashAlgorithmName.SHA512,
            _keySize);
        return CryptographicOperations.FixedTimeEquals(inputHash, hash);
    }
    
    private string GenerateToken(AccountDto account)
    {
        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
        SigningCredentials credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: _configuration.Issuer,
            audience: _configuration.Audience,
            claims: GetClaims(account),
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: credentials
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private List<Claim> GetClaims(AccountDto account)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, account.Email),
            new Claim(ClaimTypes.Role, account.UserRole.ToString()),
            new Claim("id", account.ProfileId.ToString())
        };
        if (account.UniqueUsername != null)
        {
            claims.Add(new Claim(ClaimTypes.Name, account.UniqueUsername)); 
        }
        return claims;
    }
}