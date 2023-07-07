using TableTopFriend.Domain.AggregateStatus;
using TableTopFriend.Domain.AggregateStatus.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableTopFriend.Infrastructure.Persistence.Configurations;

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
            .OwnsOne(sk => sk.Name)
            .Property(name => name.Value)
            .HasColumnName("Name")
            .HasMaxLength(50);

        statusBuilder
            .OwnsOne(sk => sk.Description)
            .Property(description => description.Value)
            .HasColumnName("Description")
            .HasMaxLength(5000);
    }
}
