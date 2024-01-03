using HSP.Data.Configuration;
using HSP.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HSP.Data;

public class HspDbContext : IdentityDbContext<CustomUser, CustomRole, int>
{
    static HspDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public HspDbContext(DbContextOptions<HspDbContext> dbContextOptions) : base(dbContextOptions)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CustomUserConfiguration)));
        base.OnModelCreating(builder);
    }

    public virtual DbSet<CustomUser> CustomUser { get; set; }
}
