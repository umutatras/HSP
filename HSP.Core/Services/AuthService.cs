using HSP.Core.Dtos;
using HSP.Core.IServices;
using HSP.Core.Jwt;
using HSP.Data;
using HSP.Entities;
using Microsoft.AspNetCore.Identity;

namespace HSP.Core.Services;

public class AuthService : IAuthService
{
    List<CustomUser> _customUser;
    private UserManager<CustomUser> _userManager;
    private SignInManager<CustomUser> _signInManager;
    private ITokenHelper _tokenHelper;
    private readonly HspDbContext _dbContext;


    public AuthService(ITokenHelper tokenHelper, HspDbContext dbContext,
        UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
    {

        _tokenHelper = tokenHelper;
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;

    }

    public List<CustomUser> GetAll()
    {
        //_loggerService.LogInfo("GetAll User - Data Listelendi");
        return _customUser;
    }

    public async Task<TokenResult> Login(LoginDto userForLoginDto)
    {
        CustomUser? user = await GetUserByLogin(userForLoginDto);
        //user nullsa kullanıcı bulunamadı. 
        if (user == null)
        {
            throw new ApplicationException("User not found.");
        }

        var accessToken = _tokenHelper.CreateToken(user);
        UpdateRefreshToken(user, accessToken);

        return accessToken;
    }

    private async Task<CustomUser> GetUserByLogin(LoginDto userForLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
        if (user == null)
        {
            //_loggerService.LogError("Mail Adresi Yok");
            throw new ApplicationException("Böyle Bir Kayıtlı Mail Yok");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userForLoginDto.Password, true, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Şifre Yanlış");
        }

        return user;
    }

    public async Task<LoginResponse> LoginForMobile(LoginDto userForLoginDto)
    {
        var user = await _userManager.FindByEmailAsync(userForLoginDto.Email);
        if (user == null)
        {
            //_loggerService.LogError("Mail Adresi Yok");
            throw new ApplicationException("Böyle Bir Kayıtlı Mail Yok");
        }

        var result = await _signInManager.PasswordSignInAsync(user, userForLoginDto.Password, true, false);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Şifre Yanlış");
        }

        //user nullsa kullanıcı bulunamadı. 
        var userInfo = _dbContext.Users
            .Where(x => x.Id == user.Id)
            .FirstOrDefault();
        var accessToken = _tokenHelper.CreateToken(user);
        UpdateRefreshToken(user, accessToken);
        var loginResponse = new LoginResponse();

        loginResponse.FullName = userInfo.Name + " " + userInfo.Surname;
        loginResponse.Id = userInfo.Id;
        loginResponse.Token = accessToken;

        return loginResponse;
    }

    public async Task<TokenResult> Register(RegisterDto userForRegisterDto)
    {
        var user = new CustomUser
        {
            Email = userForRegisterDto.Email,
            Name = userForRegisterDto.Name,
            Surname = userForRegisterDto.Surname,
            UserName = userForRegisterDto.UserName,
        };
        var accessToken = _tokenHelper.CreateToken(user);
        UpdateRefreshToken(user, accessToken);
        var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Kayıt Başarısız");
        }
        await _signInManager.SignInAsync(user, isPersistent: false);
        return accessToken;
    }

    public async Task<RegisterResponse> RegisterForMobile(RegisterDto userForRegisterDto)
    {
        var IsCurrentUser = _userManager.Users.Any(x => x.Email == userForRegisterDto.Email);
        if (IsCurrentUser)
        {
            throw new ApplicationException("Aynı e-mail ile birden fazla kayıt oluşturulamaz.");
        }


        var user = new CustomUser
        {
            Email = userForRegisterDto.Email,
            Name = userForRegisterDto.Name,
            Surname = userForRegisterDto.Surname,
            UserName = userForRegisterDto.UserName,

        };
        var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
        if (!result.Succeeded)
        {
            throw new ApplicationException("Kayıt Başarısız");
        }

        var accessToken = _tokenHelper.CreateToken(user);
        UpdateRefreshToken(user, accessToken);

        var registerResponse = new RegisterResponse();
        registerResponse.Id = user.Id;
        registerResponse.FullName = user.Name + " " + user.Surname;
        registerResponse.Email = user.Email;
        registerResponse.Token = accessToken;

        await _signInManager.SignInAsync(user, isPersistent: false);
        return registerResponse;
    }

    private void UpdateRefreshToken(CustomUser user, TokenResult accessToken)
    {
        user.RefreshToken = _tokenHelper.RefreshToken();
        user.RefreshTokenEndDate = accessToken.AccessTokenExpiration.AddDays(3);

        accessToken.RefreshToken = user.RefreshToken;
        accessToken.RefreshTokenExpiration = user.RefreshTokenEndDate.GetValueOrDefault();

        _dbContext.SaveChanges();
    }


    public TokenResult CreateAccessToken(CustomUser customUser)
    {
        var accessToken = _tokenHelper.CreateToken(customUser);
        return accessToken;
    }

    public LoginResponse RefreshToken(string refreshToken)
    {
        var responseUser = _dbContext.CustomUser
            .Where(x => x.RefreshToken == refreshToken)
            .FirstOrDefault();
        var user = _dbContext.Users.Where(x => x.RefreshToken == refreshToken).FirstOrDefault();
        var accessToken = _tokenHelper.CreateToken(user);
        UpdateRefreshToken(user, accessToken);

        var loginResponse = new LoginResponse();
        loginResponse.Token = accessToken;

        loginResponse.Email = responseUser.Email;

        loginResponse.FullName = responseUser.Name + " " + responseUser.Surname;
        loginResponse.Id = responseUser.Id;

        return loginResponse;
    }
}
