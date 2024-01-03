using HSP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSP.Data.Configuration;

public class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> builder)
    {
        builder.HasIndex(x => x.Id);
        builder.HasMany(x => x.Expenses).WithOne(x => x.Categories).HasForeignKey(x => x.CategoryId);
    }
}
