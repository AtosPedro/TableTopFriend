using TableTopFriend.Domain.AggregateUser;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using TableTopFriend.Domain.AggregateUser.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableTopFriend.Infrastructure.Persistence.Configurations;

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
            .OwnsOne(us => us.Email)
            .Property(email => email.Value)
            .HasColumnName("Email")
            .HasMaxLength(200);

        userBuilder
            .OwnsOne(us => us.Password)
            .Property(password => password.Value)
            .HasColumnName("Password");

        userBuilder
            .OwnsOne(us => us.Password)
            .Property(password => password.Salt)
            .HasColumnName("PasswordSalt");

        userBuilder
            .OwnsOne(us => us.Validation)
            .Property(validation => validation.Value)
            .HasColumnName("Validation")
            .HasConversion(type => (int)type, value => (StatusValidation)value)
            .HasComment("0 - Not Validated, 1 - Validated");

        userBuilder
            .OwnsOne(us => us.Validation)
            .Property(validation => validation.ValidationDate)
            .HasColumnName("ValidationDate");

        userBuilder
            .HasKey(m => m.Id);

        userBuilder
            .Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => UserId.Create(value));
    }
}
