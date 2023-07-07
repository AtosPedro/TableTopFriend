using TableTopFriend.Domain.AggregateAudioEffect;
using TableTopFriend.Domain.AggregateCampaign;
using TableTopFriend.Domain.AggregateCharacter;
using TableTopFriend.Domain.AggregateSession;
using TableTopFriend.Domain.AggregateSkill;
using TableTopFriend.Domain.AggregateStatus;
using TableTopFriend.Domain.AggregateUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TableTopFriend.Infrastructure.Persistence.Interfaces;

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
