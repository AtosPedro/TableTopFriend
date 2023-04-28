namespace DDDTableTopFriend.Application.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
    Task<T> Execute<T>(Func<CancellationToken,Task<T>> getData, CancellationToken cancellationToken = default);
}
