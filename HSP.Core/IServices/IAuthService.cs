using HSP.Core.Dtos;
using HSP.Core.Jwt;
using HSP.Entities;

namespace HSP.Core.IServices;

public interface IAuthService
{
    List<CustomUser> GetAll();
    Task<TokenResult> Login(LoginDto dto);
    Task<TokenResult> Register(RegisterDto userForRegisterDto);
    TokenResult CreateAccessToken(CustomUser customUser);
    LoginResponse RefreshToken(string refreshToken);
    Task<LoginResponse> LoginForMobile(LoginDto userForLoginDto);
    Task<RegisterResponse> RegisterForMobile(RegisterDto userForRegisterDto);
}
