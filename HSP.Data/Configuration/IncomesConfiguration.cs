using HSP.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HSP.Data.Configuration;

public class IncomesConfiguration : IEntityTypeConfiguration<Incomes>
{
    public void Configure(EntityTypeBuilder<Incomes> builder)
    {
        builder.HasIndex(x => x.Id);
    }
}
