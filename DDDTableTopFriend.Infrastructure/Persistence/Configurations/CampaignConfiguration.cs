using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCampaign.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        ConfigureCampaignTable(builder);
        ConfigureCharacterIdsTable(builder);
        ConfigureSessionIdsTable(builder);
    }

    private static void ConfigureSessionIdsTable(EntityTypeBuilder<Campaign> builder)
    {
        builder.OwnsMany(m => m.SessionIds, ch =>
        {
            ch.ToTable("CampaignSessionIds");
            ch.WithOwner().HasForeignKey("CampaignId");
            ch.HasKey("Id");
            ch.Property(c => c.Value)
                .HasColumnName("SessionId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Campaign.SessionIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterIdsTable(EntityTypeBuilder<Campaign> builder)
    {
        builder.OwnsMany(m => m.CharacterIds, ch =>
        {
            ch.ToTable("CampaignCharacterIds");
            ch.WithOwner().HasForeignKey("CampaignId");
            ch.HasKey("Id");
            ch.Property(c => c.Value)
                .HasColumnName("CharacterId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Campaign.CharacterIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCampaignTable(EntityTypeBuilder<Campaign> builder)
    {
        builder.ToTable("Campaigns");
        builder.HasKey(m => m.Id);
        builder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CampaignId.Create(value));

        builder.Property(m => m.Name).HasMaxLength(100);
        builder.Property(m => m.Description).HasMaxLength(200);
        builder.Property(m => m.UserId).HasConversion(id => id.Value, value => UserId.Create(value));
    }
}
