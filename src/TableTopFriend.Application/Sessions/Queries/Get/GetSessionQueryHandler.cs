using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Application.Sessions.Common;
using TableTopFriend.Domain.AggregateSession.ValueObjects;
using TableTopFriend.Domain.Common.Errors;
using ErrorOr;
using Mapster;
using MediatR;

namespace TableTopFriend.Application.Sessions.Queries.Get;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, ErrorOr<SessionResult>>
{
    private readonly ISessionRepository _sessionRepository;
    private readonly ICachingService _cachingService;

    public GetSessionQueryHandler(
        ISessionRepository sessionRepository,
        ICachingService cachingService)
    {
        _sessionRepository = sessionRepository;
        _cachingService = cachingService;
    }

    public async Task<ErrorOr<SessionResult>> Handle(
        GetSessionQuery request,
        CancellationToken cancellationToken)
    {
        var cachedSession = await _cachingService.GetCacheValueAsync<SessionResult>(request.Id.ToString());
        if (cachedSession is not null)
            return cachedSession;

        var session = await _sessionRepository.GetById(
            SessionId.Create(request.Id),
            cancellationToken);

        if (session is null)
            return Errors.Session.NotScheduled;

        var result = session.Adapt<SessionResult>();
        await _cachingService.SetCacheValueAsync(result.Id.ToString(), request);
        return result;
    }
}
