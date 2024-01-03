using HSP.Core.Extensions;
using HSP.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace HSP.Core.Jwt;

public class JwtHelper : ITokenHelper
{
    public IConfiguration Configuration { get; }
    private TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;
    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public TokenResult CreateToken(CustomUser customUser)
    {
        _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, customUser, signingCredentials);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new TokenResult
        {
            AccessToken = token,
            AccessTokenExpiration = _accessTokenExpiration,
        };
    }

    public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, CustomUser customUser, SigningCredentials signingCredentials)
    {
        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
            );
        return jwt;
    }

    private IEnumerable<Claim> SetClaims(CustomUser customUser)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(customUser.Id.ToString());
        claims.AddEmail(customUser.Email);
        claims.AddName($"{customUser.Name} {customUser.Surname}");
        return claims;
    }

    public string RefreshToken()
    {     
        byte[] number = new byte[32];
        using (RandomNumberGenerator random = RandomNumberGenerator.Create())
        {
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

    }
}
