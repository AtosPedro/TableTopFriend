using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDDTableTopFriend.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<AudioEffect> AudioEffects { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterSheet> CharacterSheets { get; set; }
    public DbSet<Status> Statuses { get; set; }

    private readonly ApplicationDbSettings _applicationDbSettings;
    protected ApplicationDbContext(ApplicationDbSettings applicationDbSettings)
    {
        _applicationDbSettings = applicationDbSettings;
    }

    public new DbSet<T> Set<T>() where T : class => base.Set<T>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_applicationDbSettings.ConnectionString);
    }
}
