using HSP.Core.Jwt;

namespace HSP.Core.Dtos;

public class LoginResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public TokenResult? Token { get; set; }
}
