using archolosDotNet.Models.UserNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace archolosDotNet.Database.Configurations;

public class RefreshTokenConfiguration: IEntityTypeConfiguration<RefreshToken>
{
public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(r => r.id);
        builder.Property(r => r.token).HasMaxLength(200);
        builder.HasIndex(r => r.token).IsUnique();
        builder.HasOne(r => r.user).WithMany().HasForeignKey(r => r.userId);

    }
}
