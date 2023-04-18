using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        ConfigureStatusTable(builder);
    }

    private static void ConfigureStatusTable(EntityTypeBuilder<Status> statusBuilder)
    {
        statusBuilder
            .ToTable("Status");

        statusBuilder
            .HasKey(m => m.Id);

        statusBuilder
            .Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => StatusId.Create(value));

        statusBuilder
            .Property(us => us.UserId)
            .HasConversion(us => us.Value, value => UserId.Create(value));

        statusBuilder
            .Property(sk => sk.Name)
            .HasMaxLength(50);

        statusBuilder
            .Property(sk => sk.Description)
            .HasMaxLength(300);
    }
}
