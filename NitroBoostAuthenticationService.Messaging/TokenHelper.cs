using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NitroBoostAuthenticationService.Shared.Logging;

namespace NitroBoostAuthenticationService.Messaging;

public class TokenHelper
{
    private string _clientSecret { get; set; }
    private string _audience { get; set; }

    public TokenHelper(string clientSecret, string audience)
    {
        _clientSecret = clientSecret;
        _audience = audience;
    }
    
    public bool ValidateSender(string tokenString, string email)
    {
        if (tokenString == string.Empty) 
            return false;
        var token = ValidateToken(tokenString);
        
        var tokenEmail = token?.Claims.FirstOrDefault(x => x.Value == email)?.Value;
        if (tokenEmail == null)
            return false;
        return tokenEmail == email;
    }

    private JwtSecurityToken? ValidateToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        try
        {
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_clientSecret)),
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidAudience = _audience,
                ClockSkew = new TimeSpan(0, 0, 5)
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
        }
        catch (Exception e)
        {   
            Logger.Log($"UNAUTHORIZED ACCESS\r\nReason: {e.Message}\r\nToken: {token}");
            return null;
        }
    }
}