using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OneTwenty.Infrastructure.Entities;

public class UserInterest
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int InterestId { get; set; }
    public Interest Interest { get; set; }
}

internal class UserInterestEntityTypeConfiguration : IEntityTypeConfiguration<UserInterest>
{
    public void Configure(EntityTypeBuilder<UserInterest> builder)
    {
        builder.HasKey(ui => new { ui.UserId, ui.InterestId });

        builder.HasOne(ui => ui.User)
            .WithMany(u => u.UserInterests)
            .HasForeignKey(ui => ui.UserId);

        builder.HasOne(ui => ui.Interest)
            .WithMany(i => i.UserInterests)
            .HasForeignKey(ui => ui.InterestId);
    }
}