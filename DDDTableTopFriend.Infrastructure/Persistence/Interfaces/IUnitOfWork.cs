namespace DDDTableTopFriend.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
    Task Rollback();
}
