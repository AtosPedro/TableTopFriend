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
        builder.OwnsMany(campaign => campaign.SessionIds, sessionIdsBuilder =>
        {
            sessionIdsBuilder
                .ToTable("CampaignSessionIds");

            sessionIdsBuilder
                .WithOwner()
                .HasForeignKey("CampaignId");

            sessionIdsBuilder
                .HasKey("Id");

            sessionIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("SessionId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Campaign.SessionIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterIdsTable(EntityTypeBuilder<Campaign> builder)
    {
        builder.OwnsMany(campaign => campaign.CharacterIds, characterIdsBuilder =>
        {
            characterIdsBuilder
                .ToTable("CampaignCharacterIds");

            characterIdsBuilder
                .WithOwner()
                .HasForeignKey("CampaignId");

            characterIdsBuilder
                .HasKey("Id");

            characterIdsBuilder
                .Property(c => c.Value)
                .HasColumnName("CharacterId")
                .ValueGeneratedNever();

            builder
                .Metadata
                .FindNavigation(nameof(Campaign.CharacterIds))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCampaignTable(EntityTypeBuilder<Campaign> campaignBuilder)
    {
        campaignBuilder
            .ToTable("Campaigns");

        campaignBuilder
            .HasKey(m => m.Id);

        campaignBuilder
            .Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => CampaignId.Create(value));

        campaignBuilder
            .Property(m => m.Name).HasMaxLength(100);

        campaignBuilder
            .Property(m => m.Description).HasMaxLength(200);

        campaignBuilder
            .Property(m => m.UserId)
            .HasConversion(id => id.Value, value => UserId.Create(value));
    }
}
