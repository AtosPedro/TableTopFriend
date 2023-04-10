using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(us => us.FirstName).HasMaxLength(50);
        builder.Property(us => us.LastName).HasMaxLength(50);
        builder.Property(us => us.Email).HasMaxLength(200);
        builder.Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => UserId.Create(value));
    }
}
