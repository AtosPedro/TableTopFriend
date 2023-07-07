using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateCampaign.ValueObjects;
using TableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TableTopFriend.Infrastructure.Persistence.Configurations;

public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
{
    public void Configure(EntityTypeBuilder<Campaign> builder)
    {
        ConfigureCampaignTable(builder);
        ConfigureCharacterIdsTable(builder);
        ConfigureSessionIdsTable(builder);
    }

    private static void ConfigureSessionIdsTable(EntityTypeBuilder<Campaign> campaignBuilder)
    {
        campaignBuilder.OwnsMany(campaign => campaign.SessionIds, sessionIdsBuilder =>
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

            campaignBuilder
                .Metadata
                .FindNavigation(nameof(Campaign.SessionIds))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
        });
    }

    private static void ConfigureCharacterIdsTable(EntityTypeBuilder<Campaign> campaignBuilder)
    {
        campaignBuilder.OwnsMany(campaign => campaign.CharacterIds, characterIdsBuilder =>
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

            campaignBuilder
                .Metadata
                .FindNavigation(nameof(Campaign.CharacterIds))!
                    .SetPropertyAccessMode(PropertyAccessMode.Field);
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
            .OwnsOne(m => m.Name)
            .Property(name => name.Value)
            .HasColumnName("Name")
            .HasMaxLength(50);

        campaignBuilder
            .OwnsOne(m => m.Description)
            .Property(description => description.Value)
            .HasColumnName("Description")
            .HasMaxLength(5000);

        campaignBuilder
            .Property(m => m.UserId)
            .HasConversion(id => id.Value, value => UserId.Create(value));
    }
}
