using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using DDDTableTopFriend.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private static void ConfigureUsersTable(EntityTypeBuilder<User> userBuilder)
    {
        userBuilder
            .ToTable("Users");

        userBuilder
            .Property(us => us.FirstName)
            .HasMaxLength(50);

        userBuilder
            .Property(us => us.LastName)
            .HasMaxLength(50);

        userBuilder
            .Property(us => us.Email)
            .HasMaxLength(200);

        userBuilder
            .HasIndex(us => us.Email)
            .IsUnique();

        userBuilder
            .HasKey(m => m.Id);

        userBuilder
            .Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => UserId.Create(value));

        userBuilder
            .Property(us =>us.UserRole)
            .HasConversion(us => (int)us, value => (UserRole)value)
            .HasComment("0 - Admin, 1 - Free user, 2 - Premium user");

        userBuilder
            .Property(us =>us.Password)
            .HasMaxLength(600);

        userBuilder
            .Property(us =>us.PasswordSalt)
            .HasMaxLength(1000);
    }
}
