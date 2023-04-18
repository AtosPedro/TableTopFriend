using DDDTableTopFriend.Domain.AggregateAudioEffect.ValueObjects;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateSkill.ValueObjects;
using DDDTableTopFriend.Domain.AggregateStatus.ValueObjects;
using DDDTableTopFriend.Domain.AggregateUser.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDTableTopFriend.Infrastructure.Persistence.Configurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        ConfigureSkillsTable(builder);
    }

    private static void ConfigureSkillsTable(EntityTypeBuilder<Skill> skillBuilder)
    {
        skillBuilder.ToTable("Skills");

        skillBuilder
            .HasKey(m => m.Id);

        skillBuilder
            .Property(us => us.Id)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => SkillId.Create(value));

        skillBuilder
            .Property(us => us.UserId)
            .HasConversion(us => us.Value, value => UserId.Create(value));

        skillBuilder
            .Property(us => us.StatusId)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => StatusId.Create(value));

        skillBuilder.Property(us => us.AudioEffectId)
            .ValueGeneratedNever()
            .HasConversion(us => us.Value, value => AudioEffectId.Create(value) );

        skillBuilder
            .Property(sk => sk.Name)
            .HasMaxLength(50);

        skillBuilder
            .Property(sk => sk.Description)
            .HasMaxLength(300);
    }
}
