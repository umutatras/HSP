using Microsoft.AspNetCore.Identity;

namespace HSP.Entities;

public class CustomUser : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenEndDate { get; set; }
    public virtual ICollection<Expenses> Expenses { get; set; }
}
