using HSP.Entities;

namespace HSP.Core.Jwt;

public interface ITokenHelper
{
    TokenResult CreateToken(CustomUser customUser);
    string RefreshToken();
}
