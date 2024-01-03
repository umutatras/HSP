using HSP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSP.Data.Configuration;

public class CustomUserConfiguration : IEntityTypeConfiguration<CustomUser>
{
    public void Configure(EntityTypeBuilder<CustomUser> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.HasMany(x => x.Expenses).WithOne(x => x.CustomUser).HasForeignKey(x => x.UserId);
    }
}
