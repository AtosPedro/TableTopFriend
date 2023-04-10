using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        ConfigureStatusTable(builder);
    }

    private static void ConfigureStatusTable(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("Status");

        builder.HasKey(m => m.Id);
        builder.Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => StatusId.Create(value));

        builder.Property(sk => sk.Name)
            .HasMaxLength(50);

        builder.Property(sk => sk.Description)
            .HasMaxLength(300);
    }
}
