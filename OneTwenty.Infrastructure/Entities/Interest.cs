using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneTwenty.Infrastructure.Entities;

public class Interest
{
    public int InterestId { get; set; }
    public string Name { get; set; }
    public List<UserInterest> UserInterests { get; set; }
}

internal class InterestEntityTypeConfiguration : IEntityTypeConfiguration<Interest>
{
    public void Configure(EntityTypeBuilder<Interest> builder)
    {
        builder.HasIndex(q => q.Name);
    }
}