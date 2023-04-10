using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        ConfigureSkillsTable(builder);
    }

    private static void ConfigureSkillsTable(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");

        builder.Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => SkillId.Create(value));

        builder.Property(us => us.StatusId)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => StatusId.Create(value));

        builder.Property(us => us.AudioEffectId)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => AudioEffectId.Create(value));

        builder.Property(sk => sk.Name)
            .HasMaxLength(50);

        builder.Property(sk => sk.Description)
            .HasMaxLength(300);
    }
}
