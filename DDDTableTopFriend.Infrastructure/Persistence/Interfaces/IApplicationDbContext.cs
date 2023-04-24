using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<AudioEffect> AudioEffects { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<Status> Statuses { get; set; }

    DbSet<T> Set<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    public DatabaseFacade Database { get; }
}
